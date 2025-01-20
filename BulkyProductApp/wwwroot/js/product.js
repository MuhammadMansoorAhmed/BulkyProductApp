document.addEventListener('DOMContentLoaded', function() {
    loadDataTable();
});

function loadDataTable() {
    var dataTable = new DataTable('#dataTbl', {
        ajax: {
            url: '/Admin/Product/GetAll',
            dataSrc: 'data' 
        },
        columns: [
            { data: 'title' },
            { data: 'isbn' },
            { data: 'listPrice' },
            { data: 'author' },
            { data: 'category.name' },
           
        ]
    });

    console.log(dataTable.data);
}
