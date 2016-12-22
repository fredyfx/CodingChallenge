$('#reverseWord').click(function () {
    let url = $('#operationURLS').attr("data-reverseWord");
    getResponse(url);
});

$('#reverseCharacter').click(function () {
    let url = $('#operationURLS').attr("data-reverseCharacter");
    getResponse(url);
});

$('#sortingAlphabetically').click(function () {
    let url = $('#operationURLS').attr("data-sortingAlphabetically");
    getResponse(url);
});

$('#encryp').click(function () {
    let url = $('#operationURLS').attr("data-encryp");
    getResponse(url);
});

function getResponse(sUrl) {
    $.ajax({
        type: 'POST',
        url: sUrl,
        data: { paragraph: $("#paragraph").val() },
        success: function (data) {
            $("#paragraphResult").html('').html(data);
        }
    });
}

$('#calculator').click(function () {
    let sUrl = $('#operationURLS').attr("data-calculator");
    let sOriginalLoanAmount = $('#originalLoanAmount').val();
    let sLoanTerm = $('#loanTerm').val();
    let sInterestRate = $('#interestRate').val();
    $.ajax({
        type: 'POST',
        url: sUrl,
        data: {
            originalLoanAmount : sOriginalLoanAmount,
            loanTerm: sLoanTerm,
            interestRate: sInterestRate
        },
        success: function (data) {
            $("#result").html('')
                .append("MonthlyPayment: " + data.MonthlyPayment).append("<br/>");
        }
    });
});
