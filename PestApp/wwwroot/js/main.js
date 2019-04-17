var cardSuitContainer = document.getElementById("cardSuitContainer");
var addRuleButton = document.getElementById("addRuleButton");
var checkBox = document.getElementById("checkBox");


function thefunction() {
    var checkBox = document.getElementById("checkBox");
    var cardSuitContainer = document.getElementById("cardSuitContainer");

    if (checkBox.checked == false) {
        cardSuitContainer.style.display = "none";
    }
    else {
        cardSuitContainer.style.display = "inline-block";
    }
}