using System;
using System.Collections.Generic;

namespace AssignmentFrontEnd.Models;

/// <summary>
/// This the model class for transaction data
/// </summary>
public partial class Transaction
{
    public long TransactionId { get; set; }

    public DateTime TransactionDate { get; set; }

    public string FromAccount { get; set; } = null!;

    public string ToAccount { get; set; } = null!;

    public decimal Amount { get; set; }

    public string? TransactionDescription { get; set; }
}
