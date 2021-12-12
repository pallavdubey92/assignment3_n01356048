window.onload = function () {
  hideAllErrors();

  const form = document.forms.editTeacherForm;

  // add event listener for each input element
  const elements = form.elements;
  for (let i = 0; i < elements.length; i++) {
    if (elements[i].type !== "submit") {
      elements[i].onkeyup = validate;
    }
  }

  // function to be called onkeyup event for each input
  // checks html 5 validation status and changes style
  function validate() {
    if (this.checkValidity()) {
      this.classList.remove("error-input");
    } else {
      this.classList.add("error-input");
    }
  }

  form.onsubmit = function () {
    const splits = location.href.split("/");
    const teacherId = splits[splits.length - 1];
    const url = `/api/teachers/${teacherId}`;

    const teacher = {
      fName: form.fName.value,
      lName: form.lName.value,
      empNo: form.empNo.value,
      hireDate: form.hireDate.value,
      salary: form.salary.value,
    };

    const xhr = new XMLHttpRequest();
    xhr.open("PUT", url);
    xhr.setRequestHeader("Content-Type", "application/json");
    xhr.send(JSON.stringify(teacher));

    xhr.onreadystatechange = function () {
      console.log(xhr);
      if (xhr.readyState === 4) {
        if (xhr.status === 200 && xhr.response == "true") {
          location.href = `/teachers/show/${teacherId}`;
        } else {
          showAllErrors();
        }
      }
    };

    return false;
  };

  function hideAllErrors() {
    const elements = document.getElementsByClassName("err-msg");

    for (let i = 0; i < elements.length; i++) {
      elements[i].classList.add("hide");
    }
  }

  function showAllErrors() {
    const elements = document.getElementsByClassName("err-msg");

    for (let i = 0; i < elements.length; i++) {
      elements[i].classList.remove("hide");
    }
  }
};
