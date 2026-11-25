namespace Sep2026.AyeChan.ExpenseTracker.WebApi.Models;

public class MonthlyReportResponseModel
{
    public int Year { get; set; }
    public int Month { get; set; }
    public List<MonthlyReportItemModel> Items { get; set; } = new();
    public decimal TotalBudget { get; set; }
    public decimal TotalSpent { get; set; }
    public decimal TotalRemaining { get; set; }
    public int OverBudgetCount { get; set; }
}