﻿// This JS file is dedicated for the amount transfer process only.

$(document).ready(function () {
    // Select elements with the class "message"
    var varifiedFromAccount = false;
    var varifiedToAccount = false;

    //delearing the element ref 
    //to be use in this file
    const btnText = $(".btn-text");
    const btnSubmit = $("#btnSubmit");
    const txtAmount = $("#txtAmount");
    const txtFromAccount = $("#txtFromAccount");
    const txtFromName = $("#txtFromAccountName");
    const txtToAccount = $("#txtToAccount");
    const txtToName = $("#txtToAccountName");
    const txtRem = $("#txtRemarks");
    const lblToAccountStatus = $("#lblToAccountStatus");
    const lblFromAccountStatus = $("#lblFromAccountStatus");
    const frmValidationError = $('#frmValidationError');

    frmValidationError.hide();//Hiding the form level error msg

    btnText.text('Initiate Payment');

    //Client side validation for the account 
    //along with the validation account.
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


    //Updating the Button text 
    //along with amount text
    $("#txtAmount").on("input", function () {
        let amt = $(this).val();
        if (amt != null && amt != '') {
            btnSubmit.text('Initiate Payment of $' + amt);
        }
        else
            btnSubmit.text('Initiate Payment');
        
    });

    //Validation process on lost focus
    txtFromAccount.blur(function () {

        if (ValidateFromDetails()) {
            VarifyAccount(txtFromName.val(), txtFromAccount.val(),'From');
        }
        else
            SetStatus('From', false);
    });

    txtFromName.blur(function () {

        if (ValidateFromDetails()) {
            VarifyAccount(txtFromName.val(), txtFromAccount.val(), 'From');
        }
        else
            SetStatus('From', false);
    });

    txtToAccount.blur(function () {

        if (ValidateToDetails()) {
            VarifyAccount(txtToName.val(), txtToAccount.val(),'To');
        }
        else
            SetStatus('To', false);
    });

    txtToName.blur(function () {

        if (ValidateToDetails()) {
            VarifyAccount(txtToName.val(), txtToAccount.val(), 'To');
        }
        else
            SetStatus('To', false);
    });


    //Form submit process
    $("#trans-form").submit(function (event) {

        event.preventDefault();
        let valid = ValidateForm();
        if (valid)
            SubmitTransaction();
    });


    //this the account varification AJAX method
    function VarifyAccount(accountName, accountNo, accountType) {

        $.ajax({
            url: 'https://localhost:7242/api/Accounts/validate?accountNumber=' + accountNo + '&accountHolderName=' + accountName, // The URL to send the request to
            type: 'GET', // HTTP method (GET, POST, PUT, DELETE, etc.)
            success: function (response) {
                // Code to execute if the request is successful
                SetStatus(accountType,response);
            },
            error: function (xhr, status, error) {
                // Code to execute if the request fails
                SetStatus(accountType, false);
                console.error("Error:", error);
                
            }
        });
    }

    //This method is for the form level input validation
    function ValidateFromDetails() {
        const lettersOnlyRegex = /^[A-Za-z\s]+$/;
        const noRegex = /^\d{12}$/;
        let isValid = true;
        const strFromAccount = txtFromAccount.val();
        const strFromName = txtFromName.val();

        $('.from-error').remove();

        if (!noRegex.test(strFromAccount)) {
            $("<label class='error-message from-error' style='color: red;'>Bank account number must be exactly 12 digits</label>")
                .insertAfter("#txtFromAccount");
            isValid = false;
        }

        if (!lettersOnlyRegex.test(strFromName)) {
            $("<label class='error-message from-error' style='color: red;'>Name field should have only charecter</label>")
                .insertAfter("#txtFromAccountName");
            isValid = false;
        }

        return isValid;
    }


    //This method for validation the amount input
    function ValidateAmount() {
        const amountRegex = /^\d+(\.\d{1,2})?$/;
        let isValid = true;

        if (txtAmount.val() == null && txtAmount.val() == '')
            return false;

        const amt = txtAmount.val();

        $('.amt-error').remove();

        if (!amountRegex.test(amt) || (parseFloat(amt) <=0 )) {
            $("<label class='error-message amt-error' style='color: red;'>Invalid amount format. Only two decimal places are allowed and it must be greater than zero.</label>")
                .insertAfter("#txtAmount");
            return false;
        }

        return true;

    }


    function ValidateToDetails() {

        const lettersOnlyRegex = /^[A-Za-z\s]+$/;
        const noRegex = /^\d{12}$/;
        let isValid = true;
        const strToAccount = txtToAccount.val();
        const strToName = txtToName.val();

        $('.to-error').remove();

        if (!noRegex.test(strToAccount)) {
            $("<label class='error-message to-error' style='color: red;'>Bank account number must be exactly 12 digits</label>")
                .insertAfter("#txtToAccount");
            isValid = false;
        }

        if (!lettersOnlyRegex.test(strToName)) {
            $("<label class='error-message to-error' style='color: red;'>Name field should have only charecter</label>")
                .insertAfter("#txtToAccountName");
            isValid = false;
        }

        return isValid;
    }

    //Method for form validation
    function ValidateForm() {

        if (ValidateFromDetails() && ValidateToDetails() && ValidateAmount())
            return true;
        else
            return false;
    }


    //This the AJAX method for adding a new transaction
    function SubmitTransaction() {

        let amountPayable = 0; 
        let rem = '';

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
                    $('#trans-form')[0].reset();
                    frmValidationError.hide();
                    Swal.fire({
                        position: "center",
                        icon: "success",
                        title: "Your transaction saved successfully!!",
                        showConfirmButton: false,
                        timer: 1500
                    });
                    $("html, body").animate({ scrollTop: 0 }, "slow");
                },
                error: function (xhr, status, error) {
                    // Handle error response
                    frmValidationError.show();
                    $("#frmValidationError p").text("Error : " + xhr.responseText); 
                    $("html, body").animate({ scrollTop: 0 }, "slow");
                }
            });
        }
        
    }

    //This method is for setting the account validation 
    //status for both the accounts.
    function SetStatus (accountType, response) {
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