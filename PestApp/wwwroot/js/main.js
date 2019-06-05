


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

var amountBox = document.getElementById("amountBox");
var typeSelect = document.getElementById("typeSelect")

function HideAndClearAmountBox() {
    var selectedOption = typeSelect.options[typeSelect.selectedIndex];
    if (selectedOption.id.toString() == "amountless") {
        amountBox.style.display = "none";
        amountBox.textContent = null;
    }
    else {
        amountBox.style.display = "inline-block";
    }
}