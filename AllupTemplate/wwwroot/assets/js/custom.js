$(document).ready(function () {

    ('addbasket').click(function (e) {
        e.preventDefault();

        let url = $(this).attr('href');

        fetch(url)
            .then(res => {
                return res.text()
            }).then(data => {
                $('.header-cart').html(data)
            })

        

    })


    $('.search').keyup (function () {

        let search = $(this).val();
        let categoryId = $('.category').val();

        if (search.length >= 3) {
            fetch('/poroduct/search?search=' + search + '&categoryId=' + category)
                .then(res => {
                    return res.text()
                })
                .then(data => {
                    $('.searchBody').html(data)
                })
        }
        else {
            $('.searchBody').html(''); 
        }
    })

    $('.productModal').click(function (e) {
        e.preventDefault();

        let url = $(this).attr('href');

        fetch(url)
            .then(res => {
                return res.json()
            })
            .then(data => {
                console.log(data);
            }}
    })
})