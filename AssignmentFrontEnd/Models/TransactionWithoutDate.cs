using System;
using System.Collections.Generic;

namespace AssignmentFrontEnd.Models;

/// <summary>
/// This is a model class used for amount transfer process, 
/// this structure will not be used for data save. 
/// It is a kind of ViewModel.
/// </summary>
public partial class TransactionWithoutDate
{
    public long TransactionId { get; set; }
    public string FromAccount { get; set; } = null!;
    public string FromAccountHolderName { get; set; } = null!;

    public string ToAccount { get; set; } = null!;
    public string ToAccountHolderName { get; set; } = null!;

    public decimal Amount { get; set; }
    public string? TransactionDescription { get; set; }
}
