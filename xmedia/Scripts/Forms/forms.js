
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

        let mainDiv = document.createElement('div');
        mainDiv.classList.add('duplicate-container');

        let divTitle = document.createElement('div');
        divTitle.classList.add('duplicate-header');

        let title = document.createElement('p');
        if (isOriginal) {
            title.textContent = 'Original';
        }
        else {
            title.textContent = 'Duplicate ' + num;
        }
        divTitle.appendChild(title);


        let divContent = document.createElement('div');
        divContent.setAttribute('class', 'duplicate-content js-duplicate-content');

        let colorLabel = document.createElement('label');
        colorLabel.textContent = 'Color';
        divContent.appendChild(colorLabel);

        let colorSelect = document.createElement('select');
        colorSelect.classList.add('duplicateColor-select');
        divContent.appendChild(colorSelect);

        let paperTypeLabel = document.createElement('label');
        paperTypeLabel.textContent = 'Paper Type';
        divContent.appendChild(paperTypeLabel);

        let paperTypeSelect = document.createElement('select');
        paperTypeSelect.setAttribute('class', 'duplicatePaperType-select js-duplicatePaperType-select');
        let paperType = document.querySelector('.js-paperType-select');
        paperTypeSelect.innerHTML = paperType.innerHTML;

        if (num == 0) {
            //original copy
            paperTypeSelect.setAttribute('disabled', 'disabled');
            paperTypeSelect.classList.remove('js-duplicatePaperType-select');
            paperTypeSelect.classList.add('js-original-duplicate');

            let selectedValue = paperType.options[paperType.selectedIndex].value;

            paperTypeSelect.value = selectedValue;
        }
        divContent.appendChild(paperTypeSelect);

        let unitCostLabel = document.createElement('small');
        unitCostLabel.setAttribute('class', 'js-unitCost-label unitCost-label');
        unitCostLabel.textContent = 'Unit Cost:';
        divContent.appendChild(unitCostLabel);

        let unitCostValue = document.createElement('input');
        unitCostValue.setAttribute('class', 'js-unitCost-value unitCost-value');
        unitCostValue.setAttribute('name', 'duplicate-unitCost');
        unitCostValue.value = '';
        unitCostValue.setAttribute('required', 'required');
        unitCostValue.setAttribute('data-value', 0);
        if (isOriginal) {
            unitCostValue.classList.add('original-value');
            unitCostValue.removeAttribute('required');
        }
        divContent.appendChild(unitCostValue);

        mainDiv.appendChild(divTitle);
        mainDiv.appendChild(divContent);

        return mainDiv;
    }
}


var hideShowPadding = function (el) {

    let paddingContainer = document.querySelector('.js-paddingGlue-container');

    paddingContainer.innerHTML = '';

    if (el.value == 2) {

        paddingContainer.appendChild(addPaddingOpt());
    }


    function addPaddingOpt() {

        let mainDiv1 = document.createElement('div');
        mainDiv1.setAttribute('style', 'text-align:right');

        let container1 = document.createElement('div');
        container1.setAttribute('class', 'padding-container');

        let label1 = document.createElement('label');
        label1.textContent = 'No of Set Per Pad';
        container1.appendChild(label1);

        let select1 = document.createElement('select');
        select1.setAttribute('class', 'duplicateColor-select js-no-ofSet-perPad');
        select1.setAttribute('required', 'required');

        let setperPad = [100, 200, 300, 400, 500, 600, 700, 800, 900, 1000];

        setperPad.forEach((item) => {

            let option1 = document.createElement('option');
            option1.value = item;
            option1.textContent = item;

            select1.appendChild(option1);
        });

        container1.appendChild(select1);


        let container2 = document.createElement('div');
        container2.setAttribute('class', 'padding-container');

        let label2 = document.createElement('label');
        label2.textContent = 'Which Side ?';
        container2.appendChild(label2);

        let select2 = document.createElement('select');
        select2.setAttribute('class', 'duplicateColor-select js-padSide');
        select2.setAttribute('required', 'required');

        let option2_1 = document.createElement('option');
        option2_1.value = '1';
        option2_1.textContent = 'Short Side';
        select2.appendChild(option2_1);

        let option2_2 = document.createElement('option');
        option2_2.value = '2';
        option2_2.textContent = 'Long Side';
        select2.appendChild(option2_2);

        container2.appendChild(select2);

        mainDiv1.appendChild(container1);
        mainDiv1.appendChild(container2);

        return mainDiv1;
    }

};

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

        if (this.value > 0) {
            document.querySelector('.js-no-ofDuplicates').removeAttribute('disabled');

            let originalDuplicate = document.querySelector('select.js-original-duplicate');

            if (originalDuplicate) {
                originalDuplicate.value = this.value;
            }
        }

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

        if (this.classList.contains('has-error')) {
            this.classList.remove('has-error');
        }

        computePrice();
    });

    document.querySelector('.js-padding-select').addEventListener('change', function (e) {
        addSelectedAndChecked(this);
        hideShowPadding(this);
        computePrice();
    })

    document.querySelector('.js-duplicate-container').addEventListener('change', function (e) {

        if (e.target.matches('.js-duplicatePaperType-select')) {
            //console.log('duplicate');
            //console.log(e.target.options[e.target.selectedIndex].textContent);

            computePrice(e.target);
        }

    });

    document.querySelector('.js-submitBtn').addEventListener('click', function (e) {
        formAddToCart();
    });
}


var addSelectedAndChecked = function (el) {

    if (el.value > 0) {

        if (el.classList.contains('has-error')) {
            el.classList.remove('has-error');
        }

        el.classList.add('selected');
        el.closest('.prodDetsCont-divCont-02-config').querySelector('i').classList.add('checked');
    }
    else {
        el.classList.remove('selected');
        el.closest('.prodDetsCont-divCont-02-config').querySelector('i').classList.remove('checked');
    }
}

var computePrice = function (paperType = null) {

    // PAPER SIZES
    let paperSizeSelect = document.querySelector('.js-paperSize-select');
    let divisorFactor = paperSizeSelect.options[paperSizeSelect.selectedIndex].getAttribute('data-DivisorFactor');
    let laborFactor = paperSizeSelect.options[paperSizeSelect.selectedIndex].getAttribute('data-LaborFactor');


    // PAPER TYPE AND ITS CORRESPONDING COST
    let paperTypeSelect = null;
    if (paperType) {
        paperTypeSelect = paperType
    }
    else {
        paperTypeSelect = document.querySelector('.js-paperType-select');
    }
    let paperCostAtA3 = paperTypeSelect.options[paperTypeSelect.selectedIndex].getAttribute('data-paperCost');
    let laborCostAtA3 = paperTypeSelect.options[paperTypeSelect.selectedIndex].getAttribute('data-laborCost');
    let printCostBW = paperTypeSelect.options[paperTypeSelect.selectedIndex].getAttribute('data-PrintCostBW');
    let printCostColored = paperTypeSelect.options[paperTypeSelect.selectedIndex].getAttribute('data-PrintCostColored');

    // NUMBER OF DUPLICATES
    let duplicatesSelect = document.querySelector('.js-no-ofDuplicates');

    // paper quantity
    let paperQuantitySelect = document.querySelector('.js-paperQuantity-select');
    let quantityFactor = paperQuantitySelect.options[paperQuantitySelect.selectedIndex].getAttribute('data-quantityfactor');
    let quantityValue = paperQuantitySelect.options[paperQuantitySelect.selectedIndex].textContent;

    // PRINT COLOR
    // 1 => BLACK&WHITE
    // 2 => COLORED
    let printColorSelect = document.querySelector('.js-printcolor-select');

    //PADDING GLUE SELECT
    let paddingCostSelect = document.querySelector('.js-padding-select');

    let totalPriceInput = document.querySelector('.js-prod-totalPrice');
    let unitPriceInput = document.querySelector('.js-prod-unitPrice');


    //set total Price to always zero
    totalPriceInput.value = 'Php 0.00';
    totalPriceInput.setAttribute('data-value', 0.00);

    if (paddingCostSelect.value == 2) {
        // add Php 15.00 if it includes padding glue
        totalPriceInput.value = 'Php 15.00';
        totalPriceInput.setAttribute('data-value', 15.00);
    }

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
        
        if (paperType) {

            let cost = parseFloat(unitCost).toFixed(2).toString();

            paperType.closest('.js-duplicate-content').querySelector('.js-unitCost-value').value = 'Php' + cost;
            paperType.closest('.js-duplicate-content').querySelector('.js-unitCost-value').setAttribute('data-value', cost);

        }
        else {

            if (duplicatesSelect.value > 0) {

                document.querySelector('input.original-value').value = 'Php ' + parseFloat(unitCost).toFixed(2).toString();
                document.querySelector('input.original-value').setAttribute('data-value', parseFloat(unitCost).toFixed(2).toString());
            }

            unitPriceInput.value = 'Php ' + parseFloat(unitCost).toFixed(2).toString();
            unitPriceInput.setAttribute('data-value', parseFloat(unitCost).toFixed(2).toString());
        }

        // PAPER QUANTITY
        if (paperQuantitySelect.value > 0 && parseInt(unitCost) > 0) {

            let total = 0;

            total += (parseFloat(quantityValue) * parseFloat(quantityFactor)) * unitCost;

            //for duplicates
            if (duplicatesSelect.value > 0) {

                document.querySelectorAll('.js-duplicate-content').forEach((item) => {

                    let duplicateCost = item.querySelector('.js-unitCost-value');

                    if (!duplicateCost.classList.contains('original-value') && duplicateCost.value !== '') {

                        //console.log(parseInt(duplicateCost.textContent));

                        total += (parseFloat(quantityValue) * parseFloat(quantityFactor)) * parseFloat(duplicateCost.value.slice(3));
                    }

                });
            }

            if (parseFloat(totalPriceInput.getAttribute('data-value')) > 0) {
                total += parseFloat(totalPriceInput.getAttribute('data-value'));
            }

            totalPriceInput.value = 'Php ' + parseFloat(total).toFixed(2).toString();
            totalPriceInput.setAttribute('data-value', parseFloat(total).toFixed(2).toString());
        }
        else {
            let total = 0;

            let cost = parseFloat(unitPriceInput.getAttribute('data-value')) || 0;

            if (parseFloat(totalPriceInput.getAttribute('data-value')) > 0) {

                total += parseFloat(totalPriceInput.getAttribute('data-value')) + parseFloat(cost);
            }

            //console.log(total);

            totalPriceInput.value = 'Php ' + parseFloat(total).toFixed(2).toString();

            totalPriceInput.setAttribute('data-value', total.toString());
        }
    }
    else {
        unitPriceInput.value = 'Php 0.00';
        unitPriceInput.setAttribute('data-value', 0.00);

        if (duplicatesSelect.value > 0) {
            document.querySelectorAll('.js-unitCost-value').forEach((item) => {
                item.value = 'Php 0.00';
                item.setAttribute('data-value', '0.00');
            })
        }
    }

};


var formAddToCart = function () {

    let form = document.querySelector('.js-prod-form');

    let error = 0;

    form.querySelectorAll('select[required], input[required]').forEach((item) => {

        //remove errors first
        item.classList.remove('has-error');

        if (item.type) {
            if (item.type == 'file') {
                item.closest('td').removeAttribute('style');
            }
        }

        //select element
        if (item.tagName == 'SELECT') {
            if (item.value == 0) {
                error += 1;
                item.classList.add('has-error');

                //console.log(item);
            }
        }
        //input element
        else {

            if (item.type == 'file') {
                if (!item.value) {
                    //console.log('test');
                    error += 1;
                    item.closest('td').setAttribute('style', 'border: 1px solid #FF0000');
                }
            }
            else {
                if (parseInt(item.getAttribute('data-value')) == 0) {
                    error += 1;
                    item.classList.add('has-error');
                }
            }

        }

    });


    console.log(error);

    if (error == 0) {
        // proceed to save the data into session
        
        let duplicates = [];

        if (form.querySelector('.js-no-ofDuplicates').value > 0) {
            form.querySelectorAll('.duplicate-container').forEach((item) => {
                if (!item.querySelector('input').classList.contains('original-value')) {
                    let obj = {
                        DuplicateColor: item.querySelector('.duplicateColor-select').value || 0,
                        DuplicatePaperType: item.querySelector('.duplicatePaperType-select').value || 0,
                        DuplicateUnitCost: item.querySelector('input[name=duplicate-unitCost]').getAttribute('data-value')
                    };

                    duplicates.push(obj);
                }
            });
        }


        let orderFormsData = new FormData();
        orderFormsData.append('UsersID', localStorage.getItem('UserID') || 0);
        orderFormsData.append('OrderID', 1);
        orderFormsData.append('TotalPrice', form.querySelector('.js-prod-totalPrice').getAttribute('data-value'));
        orderFormsData.append('OrderStatusID', 1);
        orderFormsData.append('Orderforms.FormsPaperSizesRef_ID', form.querySelector('.js-paperSize-select').value);
        orderFormsData.append('Orderforms.PaperTypeRef_ID', form.querySelector('.js-paperType-select').value);
        orderFormsData.append('Orderforms.PaperColorRef_ID', form.querySelector('.js-printcolor-select').value);
        orderFormsData.append('Orderforms.PaddingGlue_ID', form.querySelector('.js-padding-select').value);
        orderFormsData.append('Orderforms.hasPaddingGlue', 1);
        orderFormsData.append('Orderforms.NoOfSetPad', form.querySelector('.js-no-ofSet-perPad') != null ? form.querySelector('.js-no-ofSet-perPad').value : 0);
        orderFormsData.append('Orderforms.PadSide', form.querySelector('.js-padSide') != null ? form.querySelector('.js-padSide').value : 0);
        orderFormsData.append('Orderforms.UnitPrice', form.querySelector('.js-prod-unitPrice').getAttribute('data-value'));
        orderFormsData.append('Orderforms.Quantity', form.querySelector('.js-paperQuantity-select').options[form.querySelector('.js-paperQuantity-select').selectedIndex].textContent);
        orderFormsData.append('Orderforms.hasDuplicate', 0);

        //for (var pair of orderFormData.entries()) {
        //    console.log(pair[0] + ', ' + pair[1]);
        //}


        let imgCompanyLogo = form.querySelector('#js-companyLogoImg').files[0];

        if (localStorage.getItem('UserID')) {

            orderFormsData.append('Orderforms.File', imgCompanyLogo);

            //save then redirect to cart
            console.log('save and Redirect to Cart');

            saveOrder(orderFormsData);
        }
        else {

            console.log('Redirect to login');

            getBase64Image(imgCompanyLogo, function (base64String) {

                //console.log(base64String);
                orderFormsData.append('Orderforms.File', base64String);

                var object = {};
                orderFormsData.forEach((value, key) => object[key] = value);
                var json = JSON.stringify(object);

                localStorage.setItem('Cart', json);

                window.location.href = '/Account/Login';

            });

        }

    }
    else {
        // error occured
        console.log('Error');
    }

};


window.onload = function () {
    eventsListeners();
};