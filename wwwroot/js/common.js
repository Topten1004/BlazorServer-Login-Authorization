window.ShowToastr = (type, message) => {
    if (type === "success") {
        toastr.success(message, "Operation Successful");
    }
    if (type === "error") {
        toastr.error(message, "Operation Failed");
    }
    if (type === "swalSuc") {
        Swal.fire('Good job!', 'You clicked the button!', 'success');
    }
    if (type === "swalError") {
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Something went wrong!',
            footer: '<a href="">Why do I have this issue?</a>'
        });
    }
}