using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Sep2026.AyeChan.ExpenseTracker.WebApi.Models;

namespace Sep2026.AyeChan.ExpenseTracker.WebApi.Controllers;

// api/reports
[Route("api/[controller]")]
[ApiController]
public class ReportsController : ControllerBase
{
    private readonly string connectionString;

    public ReportsController(IConfiguration configuration)
    {
        // Read connection string from appsettings.json
        connectionString = configuration.GetConnectionString("DbConnection")!;
    }

    // GET api/reports/monthly?year=2026&month=11
    [HttpGet("monthly")]
    public IActionResult Monthly(int year, int month)
    {
        // Validate input (missing values in the query string become 0)
        if (year < 2000 || year > 2100 || month < 1 || month > 12)
        {
            return BadRequest("Invalid year or month.");
        }

        // Date range for the month: [startDate, endDate)
        var startDate = new DateTime(year, month, 1);
        var endDate = startDate.AddMonths(1);

        using var connection = new SqlConnection(connectionString);

        // Budget vs actual per active category (same query as the console ReportService)
        string query = @"SELECT c.[CategoryId],
                                c.[CategoryName],
                                ISNULL(b.[Amount], 0) AS [BudgetAmount],
                                ISNULL(e.[SpentAmount], 0) AS [SpentAmount],
                                ISNULL(b.[Amount], 0) - ISNULL(e.[SpentAmount], 0) AS [RemainingAmount]
                         FROM [dbo].[Tbl_Category] c
                         LEFT JOIN [dbo].[Tbl_Budget] b
                                ON b.[CategoryId] = c.[CategoryId]
                               AND b.[BudgetYear] = @Year
                               AND b.[BudgetMonth] = @Month
                               AND b.[DeleteFlag] = 0
                         LEFT JOIN (
                                SELECT [CategoryId], SUM([Amount]) AS [SpentAmount]
                                FROM [dbo].[Tbl_Expense]
                                WHERE [DeleteFlag] = 0
                                  AND [ExpenseDate] >= @StartDate
                                  AND [ExpenseDate] < @EndDate
                                GROUP BY [CategoryId]
                         ) e ON e.[CategoryId] = c.[CategoryId]
                         WHERE c.[DeleteFlag] = 0
                           AND (b.[BudgetId] IS NOT NULL OR e.[SpentAmount] IS NOT NULL)
                         ORDER BY c.[CategoryName]";

        var items = connection.Query<MonthlyReportItemModel>(query, new
        {
            Year = year,
            Month = month,
            StartDate = startDate,
            EndDate = endDate
        }).ToList();

        // Build response with totals
        var report = new MonthlyReportResponseModel
        {
            Year = year,
            Month = month,
            Items = items,
            TotalBudget = items.Sum(x => x.BudgetAmount),
            TotalSpent = items.Sum(x => x.SpentAmount),
            TotalRemaining = items.Sum(x => x.RemainingAmount),
            OverBudgetCount = items.Count(x => x.IsOverBudget)
        };

        return Ok(report);
    }
}