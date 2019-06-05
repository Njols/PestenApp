var cardSuitContainer = document.getElementById("cardSuitContainer");
var addRuleButton = document.getElementById("addRuleButton");
var checkBox = document.getElementById("checkBox");


function thefunction() {
    var checkBox = document.getElementById("CheckBox");
    var cardSuitContainer = document.getElementById("cardSuitContainer");

    if (checkBox.checked == false) {
        cardSuitContainer.style.display = "none";
    }
    else {
        cardSuitContainer.style.display = "inline-block";
    }
}

function hideAndClearAmountBox() {
    var amountBox = document.getElementById("amountBox");
    amountBox.style.display = "none";
    amountBox.textContent = null;
}