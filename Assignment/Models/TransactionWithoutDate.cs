using System;
using System.Collections.Generic;

namespace Assignment.Models;

/// <summary>
/// This is a model class as we are not taking the 
/// transaction date/time from user and also doing a validation
/// test for the chking valid account.
/// </summary>
public partial class TransactionWithoutDate
{
    public long TransactionId { get; set; }
    public string FromAccount { get; set; } = null!;
    public string FromAccountHolderName { get; set; } = null!; //To validate the From-Account

    public string ToAccount { get; set; } = null!;
    public string ToAccountHolderName { get; set; } = null!;// To validate the To-Account

    public decimal Amount { get; set; }
    public string? TransactionDescription { get; set; }
}
