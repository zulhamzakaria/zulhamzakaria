﻿using Microsoft.AspNetCore.Http;

namespace PlannerApp.Shared.Models;

public class PlanDetail : PlanSummary
{
    public IFormFile? CoverFile { get; set; }
    public List<ToDoItemDetail>? ToDoItems { get; set; }

}
