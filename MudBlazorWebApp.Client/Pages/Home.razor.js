window.alert2 = function(message) {
    // Your custom alert implementation here
   // console.log("Alert message:", message);

    if (window.hasOwnProperty('Blazor') && Blazor.hasOwnProperty('renderMode')) {
        console.log("Render Mode is: ", Blazor.renderMode);  
    } else {
        return 'Render Mode Unknown';
    }


};