// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function deleteFood(url,from) {
    Swal.fire({
        title: "Do you want to Delete This Food?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showDenyButton: true,
        showCancelButton: true,
        confirmButtonText: "Delete",
        denyButtonText: `No`
    }).then((result) => {
        /* Read more about isConfirmed, isDenied below */
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                method: 'POST',
                dataType: 'json',
                success: function (data) {
                    if (data.success) {
                        if (from === "details") {
                            window.location.href = "/Food/Index";
                        }
                        // Remove the deleted category row from the table
                        $('#food-row-' + data.foodId).remove();
                        Swal.fire("Item Deleted!", data.message, "success");

                    } else {
                        Swal.fire("An Error Occurred During Delete the Food!", data.message, "error");
                    }
                },
                error: function (error) {
                    Swal.fire("An Error Occurred During Connect the Server!", "Error!", "error");
                }
            });


        }
        else if (result.isDenied) {
            Swal.fire("Food do not Deleted", "", "info");
        }
    });
}