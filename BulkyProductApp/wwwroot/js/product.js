$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $("#dataTbl").DataTable({
        ajax: "/admin/product/getall",
        columns: [
            { data: "title" },
            { data: "isbn" },
            { data: "listPrice" },
            { data: "author" },
            { data: "category.name" },
            { data: "" }
        ]
    })
}