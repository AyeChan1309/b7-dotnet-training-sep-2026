namespace Sep2026.AyeChan.ExpenseTracker.WebApi.Models;

public class MonthlyReportItemModel
{
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public decimal BudgetAmount { get; set; }
    public decimal SpentAmount { get; set; }
    public decimal RemainingAmount { get; set; }

    // Calculated in C#, included in JSON output
    public bool IsOverBudget => RemainingAmount < 0;
    public string Status => IsOverBudget ? "Over Budget" : "OK";
}