//let CurrentSurvey = null;

document.addEventListener("DOMContentLoaded",async function () {

    const surveyListDiv = document.getElementById("survey-list");
    const succesDiv = document.createElement("div");
    succesDiv.id = "succes-message";
    succesDiv.style.color = "green";

    


    fetch('/Survey/ListSurveys')
        .then(response => {
            if (!response.ok) throw new Error("Network Error");
            return response.json();
        })
        .then(surveys => {
            surveyListDiv.innerHTML = "";

            surveyListDiv.appendChild(succesDiv);
            succesInfo();

            if (!surveys || surveys.length === 0) {

                const paragraph = document.createElement("p");
                paragraph.textContent = "Brak ankiet do uzupełnienia."
                surveyListDiv.appendChild(paragraph);
                return;

            }

            surveys.forEach((survey) => {

                //isCompleted(survey.id).then(completed => {

                //    if (completed) return;

                console.log(survey);
                const button = document.createElement("button");
                button.type = "button";
                button.textContent = survey.title;
                button.classList.add("btn", "btn-primary", "m-1");

                button.addEventListener("click", function () {
                    saveSurveyToSession(survey.id);
                    //CurrentSurvey = survey.Id;
                    //.then
                    window.location.href = `/Survey/SurveyCompleter?surveyId=${survey.Id}`;
                });

                surveyListDiv.appendChild(button);

                const br = document.createElement("br");
                surveyListDiv.appendChild(br);
            });
        })

})
    //.catch(error => console.error("Error fetching surveys:", error));


function saveSurveyToSession(surveyId) {
    fetch('/Survey/SaveChoosenSurvey', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(surveyId)
    })
        .then(res => {
            if (res.ok) {
                console.log("SurveyId zapisane w sesji:", surveyId);
            }
        })
        .catch(err => console.error("Error saving survey:", err));
}

//async function isCompleted(surveyId) {

//    try {
//        const response = await fetch(`/Survey/IsCompleted?surveyId=${surveyId}`);
//        const result = await response.json();
//        console.log(result);
//        return result;
//    } catch (err) {
//        console.error(err);
//        return;
//    }
//}

function succesInfo() {
    const succesDiv = document.getElementById("succes-message");
    const urlParams = new URLSearchParams(window.location.search);

    if (urlParams.get("success") === "1") {

        succesDiv.textContent = "Ankieta ukończona pomyślnie!"
        console.log("udało się!")

    }

}