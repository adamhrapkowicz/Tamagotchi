let dragonId = null;
let dragonStatus = null;
let dragonName;
let dragonMessage;
let deathReason;

$("document").ready(async function () {

    $("#nameForm").submit(async function(event){
        dragonName = (await $("#dragonsName").val());
        $("#nameForm").hide();
        $("#statusTable, #feedBtn, #petBtn, #dragonPic").show();

        event.preventDefault();

        try {
            let response = await fetch("https://localhost:59039/TamagotchiApi/"
                + dragonName, {method: 'POST'});
            dragonId = (await response.json()).dragonId;

            setInterval(async function() {
                let statusResponse = await fetch("https://localhost:59039/TamagotchiApi/"
                    + dragonId, {method: 'GET'});
                dragonStatus = await statusResponse.json()

                let ageGroup;
                switch (dragonStatus.ageGroup){
                    case 0:
                        ageGroup = "Baby";
                        break;
                    case 1:
                        ageGroup = "Child";
                        break;
                    case 2:
                        ageGroup = "Teen";
                        break;
                    case 3:
                        ageGroup = "Adult";
                        break;
                    case 4:
                        ageGroup = "Senior";
                        break;
                    default:
                        ageGroup = "";
                }
                $("#petName").html(dragonStatus.dragonName)
                $("#age").html(dragonStatus.age)
                $("#ageGroup").html(ageGroup)
                $("#happiness").html(dragonStatus.happiness)
                $("#feedometer").html(dragonStatus.feedometer)

                if (dragonStatus.feedometer <= 0 || dragonStatus.happiness <= 0){
                    $("#statusTable, #feedBtn, #petBtn").hide();
                    $("#dragonPic").attr("src", "photos/grave.jpg").attr("alt", "grave")
                    if (dragonStatus.feedometer <= 0)
                    {
                        deathReason = "feed it enough!";
                    }
                    else if (dragonStatus.happiness <= 0)
                    {
                        deathReason = "love it enough!";
                    }
                    $("#myBanner").html(dragonStatus.dragonName + " has just died," +
                        " because you didn't " + deathReason);
                }
            }, 1000)

            $("#feedBtn").click(async function(){
                let feedingResponse = await fetch("https://localhost:59039/TamagotchiApi/feed/"
                    + dragonId, {method: 'PUT'});
                let feedingStatus = await feedingResponse.json()

                if (feedingStatus.success === true){
                    dragonMessage = "That was yummy!";
                }
                else if (feedingStatus.success !== true && feedingStatus.reason === 1){
                    dragonMessage = "I'm not hungry!";
                }
                $("#dragonsMessage").html(dragonMessage);
                let toastElList = [].slice.call($(".toast"));
                let toastList = toastElList.map(function (toastEl) {
                    return new bootstrap.Toast(toastEl)
                });

                toastList.forEach(toast => toast.show(1000))
            });

            $("#petBtn").click(async function(){
                let pettingResponse = await fetch("https://localhost:59039/TamagotchiApi/pet/"
                    + dragonId, {method: 'PUT'});
                let pettingStatus = await pettingResponse.json();

                if (pettingStatus.success === true){
                    dragonMessage = "I love you!";
                }
                else if (pettingStatus.success !== true && pettingStatus.reason === 1){
                    dragonMessage = "Leave me alone!";
                }
                $("#dragonsMessage").html(dragonMessage);
                let toastElList = [].slice.call($(".toast"));
                let toastList = toastElList.map(function (toastEl) {
                    return new bootstrap.Toast(toastEl)
                });
                toastList.forEach(toast => toast.show(1000))
            });
        }
        catch(err){
            await fetch("http://localhost:63342/Tamagotchi/Tamagotchi/Site/index.html");
            alert("You must name your dragon!");
        }
    });
});
