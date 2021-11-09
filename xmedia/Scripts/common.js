
fetch(AppGlobal.baseUrl + 'Forms/GetFormCommon/',
    {
        headers: {
            'Content-Type': 'application/json'
        },
    }
    )
    .then(data => data.json())
    .then(function (data) {
        console.log(data);
    })
    .catch(function (error) {
        console.log(error);
})

