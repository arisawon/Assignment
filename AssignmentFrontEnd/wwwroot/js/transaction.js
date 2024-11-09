

$(document).ready(function () {
    // Select elements with the class "message"
    var varifiedFromAccount = false;
    var varifiedToAccount = false;


    var btnText = $(".btn-text");
    var btnSubmit = $("#btnSubmit");
    var txtAmount = $("#txtAmount");
    var txtFromAccount = $("#txtFromAccount");
    var txtFromName = $("#txtFromAccountName");
    var txtToAccount = $("#txtToAccount");
    var txtToName = $("#txtToAccountName");
    var txtRem = $("#txtRemarks");
    var lblToAccountStatus = $("#lblToAccountStatus");
    var lblFromAccountStatus = $("#lblFromAccountStatus");

    txtAmount.val('0');
    btnText.text('Initiate Payment');

    if (varifiedToAccount) {
        lblToAccountStatus.css("color", "limegreen");
        lblToAccountStatus.text('Valid account');
    }
    else {
        lblToAccountStatus.css("color", "red");
        lblToAccountStatus.text('Not a valid account');
    }

    if (varifiedFromAccount) {
        lblFromAccountStatus.css("color", "limegreen");
        lblFromAccountStatus.text('Valid account');
    }
    else {
        lblFromAccountStatus.css("color", "red");
        lblFromAccountStatus.text('Not a valid account');
    }


    $("#txtAmount").on("input", function () {
        let amt = $(this).val();
        if (amt != null && amt != '') {
            btnText.text('Initiate Payment of $' + amt);
        }
        else
            btnText.text('Initiate Payment');
        
    });

    txtFromAccount.blur(function () {

        if (txtFromName.val() != null && txtFromName.val() != '' && txtFromAccount.val() != null && txtFromAccount.val() != '') {
            VarifyAccount(txtFromName.val(), txtFromAccount.val(),'From');
        }
        else
            setStatus('From', false);
    });

    txtFromName.blur(function () {

        if (txtFromName.val() != null && txtFromName.val() != '' && txtFromAccount.val() != null && txtFromAccount.val() != '') {
            VarifyAccount(txtFromName.val(), txtFromAccount.val(), 'From');
        }
        else
            setStatus('From', false);
    });

    

    txtToAccount.blur(function () {

        if (txtToName.val() != null && txtToName.val() != '' && txtToAccount.val() != null && txtToAccount.val() != '') {
            VarifyAccount(txtToName.val(), txtToAccount.val(),'To');
        }
        else
            setStatus('To', false);
    });

    txtToName.blur(function () {

        if (txtToName.val() != null && txtToName.val() != '' && txtToAccount.val() != null && txtToAccount.val() != '') {
            VarifyAccount(txtToName.val(), txtToAccount.val(), 'To');
        }
        else
            setStatus('To', false);
    });
    btnSubmit.click(function () {
        SubmitTransaction();
    });

    function VarifyAccount(accountName, accountNo, accountType) {

        $.ajax({
            url: 'https://localhost:7242/api/Accounts/validate?accountNumber=' + accountNo + '&accountHolderName=' + accountName, // The URL to send the request to
            type: 'GET', // HTTP method (GET, POST, PUT, DELETE, etc.)
            success: function (response) {
                // Code to execute if the request is successful
                setStatus(accountType,response);
            },
            error: function (xhr, status, error) {
                // Code to execute if the request fails
                setStatus(accountType, false);
                console.error("Error:", error);
                
            }
        });
    }

    function SubmitTransaction() {

        let amountPayable = 0; 
        let rem = '';

        if (txtToName.val() != null && txtToName.val() != '' && txtToAccount.val() != null && txtToAccount.val() != '') {
            VarifyAccount(txtToName.val(), txtToAccount.val(), 'To');
        }
        if (txtFromName.val() != null && txtFromName.val() != '' && txtFromAccount.val() != null && txtFromAccount.val() != '') {
            VarifyAccount(txtFromName.val(), txtFromAccount.val(), 'From');
        }

        if (txtAmount.val() != null && txtAmount.val() != '')
            amountPayable = parseFloat(txtAmount.val());

        if (txtRem.val() != null && txtRem.val() != '')
            rem = txtRem.val();
        else
            rem = '';


        if (varifiedFromAccount && varifiedToAccount && amountPayable > 0) {
            $.ajax({
                url: 'https://localhost:7242/api/Transactions/add-transaction', // Example API endpoint
                type: 'POST', // Use POST method
                contentType: 'application/json', // Specify content type if sending JSON
                data: JSON.stringify({ // Data to send in the request body, serialized as JSON
                    transactionId: 0,
                    fromAccount: txtFromAccount.val().trim(),
                    fromAccountHolderName: txtFromName.val().trim(),
                    toAccount: txtToAccount.val().trim(),
                    toAccountHolderName: txtToName.val().trim(),
                    amount: amountPayable,
                    transactionDescription: rem
                }),
                success: function (response) {
                    // Handle successful response
                    console.log("Post created successfully:", response);
                },
                error: function (xhr, status, error) {
                    // Handle error response
                    console.error("Error creating post:", error);
                }
            });
        }
        
    }

    function setStatus (accountType, response) {
        if (accountType == 'To') {
            if (response) {
                varifiedToAccount = true;
                lblToAccountStatus.css("color", "limegreen");
                lblToAccountStatus.text('Valid account');
            }
            else {
                varifiedToAccount = false;
                lblToAccountStatus.css("color", "red");
                lblToAccountStatus.text('Not a valid account');
            }
        }
        if (accountType == 'From') {
            if (response) {
                varifiedFromAccount = true;
                lblFromAccountStatus.css("color", "limegreen");
                lblFromAccountStatus.text('Valid account');
            }
            else {
                varifiedFromAccount = false;
                lblFromAccountStatus.css("color", "red");
                lblFromAccountStatus.text('Not a valid account');
            }
        }
    }


});