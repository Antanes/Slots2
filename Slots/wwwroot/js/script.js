let xhttp = new XMLHttpRequest();

xhttp.onreadystatechange = function () {
    if (this.readyState == 4 && this.status == 200) {
        myFunction(this.responseText)
    }
}

xhttp.open("GET", "/Drink/GetDrinks", true);
xhttp.send();

function myFunction(data) {
    console.log(data);
}