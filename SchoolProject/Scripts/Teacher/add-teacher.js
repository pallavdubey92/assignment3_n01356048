window.onload = handleLoad

function handleLoad() {
    hideAllErrors();

    const form = document.forms.addTeacherForm
    form.onsubmit = handleSubmit;

    // add event listener for each input element
    const elements = form.elements;
    for (let i = 0; i < elements.length; i++) {
        if (elements[i].type !== 'submit') {
            elements[i].onkeyup = validate;
        }
    }

    // function to be called onkeyup event for each input
    // checks html 5 validation status and changes style
    function validate() {
        if (this.checkValidity()) {
            this.classList.remove('error-input')
        } else {
            this.classList.add('error-input')
        }
    }

    function handleSubmit() {
        const newTeacher = {
            fName: form.fName.value,
            lName: form.lName.value,
            empNo: form.empNo.value,
            hireDate: form.hireDate.value,
            salary: form.salary.value,
        }

        // make xhr request
        addTeacher(newTeacher);
        return false;
    }

    function addTeacher(newTeacher) {
        // https://attacomsian.com/blog/http-requests-xhr

        const xhr = new XMLHttpRequest();

        xhr.open('POST', '/api/teachers');
        xhr.setRequestHeader('Content-Type', 'application/json');

        xhr.send(JSON.stringify(newTeacher));

        xhr.onload = function () {
            if (xhr.response == "true") {
                // navigate list page
                location.href = "/teachers";
            } else {
                alert("Failed to add teacher. Invalid data. Please check all the field again.");
                showAllErrors();
            }
        }
    }

    function hideAllErrors() {
        const elements = document.getElementsByClassName("err-msg");

        for (let i = 0; i < elements.length; i++) {
            elements[i].classList.add('hide');
        }
    }

    function showAllErrors() {
        const elements = document.getElementsByClassName("err-msg");

        for (let i = 0; i < elements.length; i++) {
            elements[i].classList.remove('hide');
        }
    }
}
