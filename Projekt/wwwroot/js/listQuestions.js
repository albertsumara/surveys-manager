//let CurrentSurvey = null;
//let CurrentQuestion = null;


document.addEventListener("DOMContentLoaded", function () {
    const questionListDiv = document.getElementById("question-list");

    getSurveyId().then(surveyId => {
        //console.log(surveyId);
        if (!surveyId) return;
        CurrentSurvey = surveyId;

        fetch(`/Survey/ListQuestions?surveyId=${surveyId}`)
            .then(response => {
                if (!response.ok) throw new Error("Network Error");
                return response.json();
            })
            .then(questions => {
                questionListDiv.innerHTML = "";

                questions.forEach(question => {
                    //console.log(question);
                    const div = document.createElement("div");
                    div.textContent = question.content;
                    questionListDiv.appendChild(div);

                    const answersDiv = document.createElement("div");
                    questionListDiv.appendChild(answersDiv);

                    listAnswers(question.id, answersDiv);

                    questionListDiv.appendChild(document.createElement("br"));
                });
            })
            .catch(error => console.error("Error fetching questions:", error));
    });
});

function getSurveyId() {
    return fetch('/Survey/GetChoosenSurvey')
        .then(res => res.json())
        .then(data => {
            //console.log(data);
            if (data.surveyId > 0) {
                //console.log("Aktualna ankieta:", data.surveyId);
                return data.surveyId;
            } else {
                return null;
            }
        });
}

function listAnswers(questionId, containerDiv) {
    fetch(`/Survey/ListAnswers?questionId=${questionId}`)
        .then(response => response.json())
        .then(answers => {
            containerDiv.innerHTML = "";
            console.log(answers);
            answers.forEach(answer => {
                console.log(answer);
                const input = document.createElement("input");
                const label = document.createElement("label");
                const br = document.createElement("br");

                input.setAttribute("type", "radio");
                input.setAttribute("name", questionId);
                //input.textContent = answer.content;

                label.appendChild(input);
                label.append(answer.content);
                
                containerDiv.appendChild(label);
                containerDiv.appendChild(br);
            });
        })
        .catch(error => console.error("Error fetching answers:", error));
}
