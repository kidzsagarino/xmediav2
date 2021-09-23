

var selectDuplicates = (function () {

    let noOFDuplicates = document.querySelector('.js-no-ofDuplicates');

    noOFDuplicates.addEventListener('change', function (e) {

        let duplicateContainer = document.querySelector('.js-duplicate-container');

        duplicateContainer.innerHTML = '';

        if (e.target.value > 0) {
            duplicateContainer.removeAttribute('style');

            duplicateContainer.appendChild(createDuplicateDiv(true));

            if (e.target.value > 1) {
                for (var i = 1; i <= e.target.value; i++) {
                    duplicateContainer.appendChild(createDuplicateDiv(false, i));
                }
            }
            else {
                duplicateContainer.appendChild(createDuplicateDiv(false, e.target.value));
            }
        }
        
    });


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

})();


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

    var customDimensionDiv = document.querySelector('.js-custom-dimension');

    let paperSizeSelect = document.querySelector('.js-paperSize-select');

    paperSizeSelect.addEventListener('change', function (e) {

        //  value is 0 for the custom size
        if (e.target.value == 0) {

            customDimensionDiv.removeAttribute('style')

        }
        else {
            customDimensionDiv.setAttribute('style', 'display:none');
        }

    });

})();


var onChangePaperSize = (function () {

    document.querySelector('.js-paperSize-select').addEventListener('change', function (e) {

        console.log('Paper Size Changed');
    });

})();

var onChangePaperType = (function () {

    document.querySelector('.js-paperType-select').addEventListener('change', function (e) {
        computePrice();
    });

})();

var onChangeDuplicates = (function () {

    document.querySelector('.js-no-ofDuplicates').addEventListener('change', function (e) {
        computePrice();
    });

})();

var onChangePaperQuantity = (function () {

    document.querySelector('.js-paperQuantity-select').addEventListener('change', function (e) {
        computePrice();
    });

})();

var computePrice = function () {

    let paperSizeSelect = document.querySelector('.js-paperSize-select');
    let sizeFactor = paperSizeSelect.options[paperSizeSelect.selectedIndex].getAttribute('data-formsizefactor');


    // paper type and its corresponding cost
    let paperTypeSelect = document.querySelector('.js-paperType-select');
    let paperCostAtA3 = paperTypeSelect.options[paperTypeSelect.selectedIndex].getAttribute('data-paperCost');
    let laborCost = paperTypeSelect.options[paperTypeSelect.selectedIndex].getAttribute('data-laborCost');
    let printCostBW = paperTypeSelect.options[paperTypeSelect.selectedIndex].getAttribute('data-PrintCostBW');
    let printCostColored = paperTypeSelect.options[paperTypeSelect.selectedIndex].getAttribute('data-PrintCostColored');

    // number of dulplicates
    let duplicatesSelect = document.querySelector('.js-no-ofDuplicates');
    let noOfDuplicates = duplicatesSelect.options[duplicatesSelect.selectedIndex].getAttribute('data-value');

    // paper quantity
    let paperQuantitySelect = document.querySelector('.js-paperQuantity-select');
    let quantityFactor = paperQuantitySelect.options[paperQuantitySelect.selectedIndex].getAttribute('data-quantityfactor');
    let quantityValue = paperQuantitySelect.options[paperQuantitySelect.selectedIndex].textContent;



    //let totalPrice = ((parseFloat(quantityValue) * parseFloat(quantityFactor)) * parseFloat(unitCost)) * parseFloat(noOfDuplicates) * parseFloat(sizeFactor);

    //let totalPriceInput = document.querySelector('.js-prod-totalPrice');
    //let unitPriceInput = document.querySelector('.js-prod-unitPrice');

    //unitPriceInput.value = 'Php ' + parseFloat(unitCost).toFixed(2).toString();
    //totalPriceInput.value = 'Php ' + parseFloat(totalPrice).toFixed(2).toString();
};