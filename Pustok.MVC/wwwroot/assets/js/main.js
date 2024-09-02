let addBasketBtns = document.querySelectorAll(".add-to-basket");

addBasketBtns.forEach(btn => {
    btn.addEventListener("click", function (e) {
        e.preventDefault();
        let url = this.getAttribute("href"); // Using 'this' to refer to the clicked button
        fetch(url)
            .then(response => {
                if (response.ok) { 
                    alert("Added to basket!");
                } else {
                    alert("Could not find book to add basket!");
                }
            })
            
    });
});
