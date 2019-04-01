var cardSelection = document.getElementById("card-selection");
var ruleSelection = document.getElementById("rule-selection");
var addRuleButton = document.getElementById("addRuleButton");
var selectContainer = document.getElementById("select-container");

addRuleButton.onclick = CreateNewElement;

function moveSelectContainer() {
    var bottomNumber = selectContainer.style.bottom.replace('px', '');
    var bottom = parseInt(bottomNumber, 10);
    selectContainer.style.bottom = '${bottom + 25}px';
}
function CreateNewElement() {
    var para = document.createElement("p");
    var card = cardSelection.value;
    var rule = ruleSelection.value;
    var node = document.createTextNode(card + " " + rule);
    para.appendChild(node);

    selectContainer.appendChild(para);
}