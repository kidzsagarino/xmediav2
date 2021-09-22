

var selectDuplicates = (function () {

    let noOFDuplicates = document.querySelector('.js-no-ofDuplicates');

    noOFDuplicates.addEventListener('change', function (e) {

        let duplicateContainer = document.querySelector('.js-duplicate-container');

        duplicateContainer.innerHTML = '';

        if (e.target.value == 0) {
            duplicateContainer.setAttribute('style', 'display:none');
        }
        else {
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


        let select = document.createElement('select');
        select.setAttribute('class', 'color-select');

        DivEl.appendChild(label);
        DivEl.appendChild(select);

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