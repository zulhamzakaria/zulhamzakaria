﻿using System.ComponentModel.DataAnnotations;

namespace PlannerApp.Shared.Models;

public class LoginRequest
{
    [Required]
    [EmailAddress]
    public string? Email { get; set; }

    [Required]
    [StringLength(6)]
    public string? Password { get; set; }
}
