namespace PlannerApp.Shared.Models;

public class ToDoItemDetail
{
    public string? Id { get; set; }
    public string? Description { get; set; }
    //public DateTime EstimationDate { get; set; }
    //public DateTime AchievedDate { get; set; }
    public bool isDone { get; set; }
    public string? planId { get; set; }
}