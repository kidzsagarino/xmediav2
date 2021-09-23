
var addDuplicateElem = function (el) {

    let duplicateContainer = document.querySelector('.js-duplicate-container');

    duplicateContainer.innerHTML = '';

    if (el.value > 0) {
        duplicateContainer.removeAttribute('style');

        duplicateContainer.appendChild(createDuplicateDiv(true));

        if (el.value > 1) {
            for (var i = 1; i <= el.value; i++) {
                duplicateContainer.appendChild(createDuplicateDiv(false, i));
            }
        }
        else {
            duplicateContainer.appendChild(createDuplicateDiv(false, el.value));
        }
    }

    function createDuplicateDiv(isOriginal, num = 0) {
        let DivEl = document.createElement('div');
        DivEl.setAttribute('class', 'duplicate-container');

        let label = document.createElement('label');

        if (isOriginal) {
            label.textContent = 'Color of Original';
        }
        else {
            label.textContent = 'Duplicate ' + num;
        }

        let selectColor = document.createElement('select');
        selectColor.setAttribute('class', 'duplicateColor-select');

        let paperTypeLabel = document.createElement('label');
        paperTypeLabel.textContent = 'Paper Type';
        let selectPaperType = document.createElement('select');
        selectPaperType.setAttribute('class', 'duplicatePaperType-select');

        DivEl.appendChild(label);
        DivEl.appendChild(selectColor);
        DivEl.appendChild(paperTypeLabel);
        DivEl.appendChild(selectPaperType);

        return DivEl;
    }
}


var withPadding = (function () {

    let paddingSelect = document.querySelector('.js-padding-select');

    let paddingContainer = document.querySelector('.padding-color-container');

    paddingSelect.addEventListener('change', function (e) {

        if (e.target.value == 1) {
            paddingContainer.setAttribute('style', 'display: none');
        }
        else {
            paddingContainer.removeAttribute('style');
        }
    });

})();

var customDimension = (function () {

    //var customDimensionDiv = document.querySelector('.js-custom-dimension');

    //let paperSizeSelect = document.querySelector('.js-paperSize-select');

    //paperSizeSelect.addEventListener('change', function (e) {

    //    //  value is 0 for the custom size
    //    if (e.target.value == 0) {

    //        customDimensionDiv.removeAttribute('style')

    //    }
    //    else {
    //        customDimensionDiv.setAttribute('style', 'display:none');
    //    }

    //});

})();


var eventsListeners = function () {

    document.querySelector('.js-paperSize-select').addEventListener('change', function (e) {

        addSelectedAndChecked(this);

        computePrice();
    });

    document.querySelector('.js-paperType-select').addEventListener('change', function (e) {

        addSelectedAndChecked(this);

        computePrice();
    });

    document.querySelector('.js-no-ofDuplicates').addEventListener('change', function (e) {

        addSelectedAndChecked(this);
        addDuplicateElem(this);

        computePrice();
    });

    document.querySelector('.js-printcolor-select').addEventListener('change', function (e) {

        addSelectedAndChecked(this);

        computePrice();
    });

    document.querySelector('.js-paperQuantity-select').addEventListener('change', function (e) {

        /*addSelectedAndChecked(this);*/

        computePrice();
    });
}


var addSelectedAndChecked = function (el) {

    if (el.value > 0) {
        el.classList.add('selected');
        el.closest('.prodDetsCont-divCont-02-config').querySelector('i').classList.add('checked');
    }
    else {
        el.classList.remove('selected');
        el.closest('.prodDetsCont-divCont-02-config').querySelector('i').classList.remove('checked');
    }
}

var computePrice = function () {

    // paper sizes
    let paperSizeSelect = document.querySelector('.js-paperSize-select');
    let divisorFactor = paperSizeSelect.options[paperSizeSelect.selectedIndex].getAttribute('data-DivisorFactor');
    let laborFactor = paperSizeSelect.options[paperSizeSelect.selectedIndex].getAttribute('data-LaborFactor');


    // paper type and its corresponding cost
    let paperTypeSelect = document.querySelector('.js-paperType-select');
    let paperCostAtA3 = paperTypeSelect.options[paperTypeSelect.selectedIndex].getAttribute('data-paperCost');
    let laborCostAtA3 = paperTypeSelect.options[paperTypeSelect.selectedIndex].getAttribute('data-laborCost');
    let printCostBW = paperTypeSelect.options[paperTypeSelect.selectedIndex].getAttribute('data-PrintCostBW');
    let printCostColored = paperTypeSelect.options[paperTypeSelect.selectedIndex].getAttribute('data-PrintCostColored');

    // number of dulplicates
    let duplicatesSelect = document.querySelector('.js-no-ofDuplicates');
    let noOfDuplicates = duplicatesSelect.options[duplicatesSelect.selectedIndex].getAttribute('data-value');

    // paper quantity
    let paperQuantitySelect = document.querySelector('.js-paperQuantity-select');
    let quantityFactor = paperQuantitySelect.options[paperQuantitySelect.selectedIndex].getAttribute('data-quantityfactor');
    let quantityValue = paperQuantitySelect.options[paperQuantitySelect.selectedIndex].textContent;

    // print color
    // 1 => Black&White
    // 2 => Colored
    let printColorSelect = document.querySelector('.js-printcolor-select');

    let totalPriceInput = document.querySelector('.js-prod-totalPrice');
    let unitPriceInput = document.querySelector('.js-prod-unitPrice');

    //config {paperSize, paperType, printColor}

    if (paperSizeSelect.value > 0 && paperTypeSelect.value > 0 && printColorSelect.value > 0) {

        console.log('ready for unit cost');

        let materialCost = parseFloat(paperCostAtA3) / parseFloat(divisorFactor);
        let laborCost = parseFloat(laborCostAtA3) * parseFloat(laborFactor);
        let printingCost = 0;
        if (printColorSelect.value == 1) {
            printingCost = parseFloat(printCostBW);
        }
        if (printColorSelect.value == 2) {
            printingCost = parseFloat(printCostColored);
        }

        let unitCost = parseFloat(materialCost + laborCost + printingCost).toFixed(2);

        unitPriceInput.value = 'Php ' + parseFloat(unitCost).toFixed(2).toString();

        if (paperQuantitySelect.value > 0) {
            let total = (parseFloat(quantityValue) * parseFloat(quantityFactor)) * unitCost;

            totalPriceInput.value = 'Php ' + parseFloat(total).toFixed(2).toString();
        }
        else {
            totalPriceInput.value = 'Php 0.00';
        }
    }
    else {
        unitPriceInput.value = 'Php 0.00';
    }

};


window.onload = function () {
    eventsListeners();
};