const RegExpDataValidationPatterns = {
    EmailAddress: /^([a-z\d\.-]+)@([a-z\d-]+).([a-z]{2,8})(\.[a-z]{2,8})$/,
    IStillLoveYou: /^([\w@-]{8,20})$/,
    ConfirmPassword: /^([\w@-]{8,20})$/,
    FirstName: /^([a-zA-Z\s]+)$/,
    LastName: /^([a-zA-Z\s]+)$/,
    CompanyName: /^([\w@-]{2,150})$/,
    MobileNo: /^(\+?\(?)([0-9\)-]{6,20})$/,
    LandlineNo: /^(\+?\(?)([0-9\)-]{6,20})$/
}


//This will be use for proper display of words
const inputNameReadableDisplay = {
    EmailAddress: "Email Address",
    IStillLoveYou: "Password",
    ConfirmPassword: "Confirm Password",
    FirstName: "First Name",
    LastName: "Last Name",
    CompanyName: "Company Name",
    LandlineNo: "Landline No",
    MobileNo: "Mobile No"
}

function Validate(targetElement, regex) {
    return regex.test(targetElement.value)
}