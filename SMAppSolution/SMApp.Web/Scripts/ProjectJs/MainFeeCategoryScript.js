$(document)
    .ready(function() {

//Prepare jtable plugin
        $('#MainFeeCategoryTable').jtable({
            title: 'Main Category List',
            paging: true, //Enable paging
            pageSize: 10, //Set page size (default: 10)
            sorting: true, //Enable sorting
            defaultSorting: 'Name ASC', //Set default sorting
            actions: {
                listAction: '/Fee/ListOfMainFeeCategory',
                deleteAction: '/Fee/DeleteOfMainFeeCategory',
                updateAction: '/Fee/UpdateOfMainFeeCategory',
                createAction: '/Fee/CreateOfMainFeeCategory'
            },
            fields: {
                Id: {
                    key: true,
                    create: false,
                    edit: false,
                    list: false
                },
                Name: {
                    title: 'Name',
                    width: '23%'
                }
            }
        });

        //Load all records when page is first shown
        $('#MainFeeCategoryTable').jtable('load');
    });
//End of jTable