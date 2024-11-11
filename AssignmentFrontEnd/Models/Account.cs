using System;
using System.Collections.Generic;

namespace AssignmentFrontEnd.Models;


/// <summary>
/// This is the model class for account
/// </summary>
public partial class Account
{
    public int AccountId { get; set; }

    public string AccountNumber { get; set; } = null!;

    public string AccountHolderName { get; set; } = null!;

    public decimal Balance { get; set; }
}
