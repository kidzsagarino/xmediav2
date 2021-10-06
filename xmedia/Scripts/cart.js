

var displayCartItem = (function () {

    let cartContainer = document.querySelector('.js-cart-container');
    cartContainer.innerHTML = '';

    if (localStorage.getItem('Cart')) {

        let data = JSON.parse(localStorage.getItem('Cart'));

        if (data.length) {

            data.forEach((item) => {
                cartContainer.appendChild(cartItemDiv());
            });

        }

    }


    function cartItemDiv() {

        let container = document.createElement('div');
        container.setAttribute('class', 'cart-item');

        let col1 = document.createElement('div');
        col1.setAttribute('class', 'cart-item-01');
        col1.innerHTML = 'Image';
        container.appendChild(col1);

        let col2 = document.createElement('div');
        col2.setAttribute('class', 'cart-item-02');
        col2.innerHTML = 'Details'
        container.appendChild(col2);

        let col3 = document.createElement('div');
        col3.setAttribute('class', 'cart-item-03');

        let col3Price = document.createElement('div');
        col3Price.setAttribute('class', 'cart-item-price');
        col3Price.innerHTML = 'Price';
        col3.appendChild(col3Price);

        let col3Opt = document.createElement('div');
        col3Opt.setAttribute('class', 'cart-item-opt');

        let edtBtn = document.createElement('button');
        edtBtn.setAttribute('class', 'cart-editBtn');
        edtBtn.textContent = 'Edit';
        col3Opt.appendChild(edtBtn);

        let rmvBtn = document.createElement('button');
        rmvBtn.setAttribute('class', 'cart-removeBtn');
        rmvBtn.textContent = 'Remove';
        col3Opt.appendChild(rmvBtn);

        col3.appendChild(col3Opt);

        container.appendChild(col3);


        return container;
    }

})();
