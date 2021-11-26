window.onload = handleLoad

function handleLoad() {
    const deleteLink = document.getElementById("deleteLink");
    deleteLink.onclick = handleDelete;
}

function handleDelete() {
    const splits = location.href.split('/');
    const teacherId = splits[splits.length - 1];
    console.log(teacherId);

    const inp = confirm("Proceed with delete?");

    if (inp === true) {
        deleteTeacher(teacherId)
    }

    return false;
}

function deleteTeacher(teacherId) {
    // https://attacomsian.com/blog/http-requests-xhr
    const xhr = new XMLHttpRequest();

    xhr.open('DELETE', `/api/teachers/${teacherId}`);

    xhr.send();

    xhr.onload = function () {
        if (xhr.response == 'true') {
            // navigate list page
            location.href = "/teachers";
        } else {
            alert("Failed to delete teacher. Please remove all associated classes before deleting.");
        }
    }
}
