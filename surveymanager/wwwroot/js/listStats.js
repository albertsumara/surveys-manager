document.addEventListener("DOMContentLoaded", async function () {
    const questionListDiv = document.getElementById("question-list");

    const surveyId = await getSurveyId();
    if (!surveyId) return;
    CurrentSurvey = surveyId;
    const stats = await getStats(CurrentSurvey);

        fetch(`/SurveyResults/GetResults?surveyId=${surveyId}`)
            .then(response => {
                if (!response.ok) throw new Error("Network Error");
                return response.json();
            })
            .then(questions => {


            });

        fetch(`/Survey/ListQuestions?surveyId=${surveyId}`)
            .then(response => {
                if (!response.ok) throw new Error("Network Error");
                return response.json();
            })
            .then(questions => {
                questionListDiv.innerHTML = "";

                questions.forEach(question => {

                    const questionDiv = document.createElement("div");
                    const questionTitle = document.createElement("h4");
                    questionTitle.textContent = question.content;
                    questionDiv.appendChild(questionTitle);
                    questionListDiv.appendChild(questionDiv);

                    listAnswers(question.id, questionDiv, stats[question.id]);

                });

            })
            .catch(error => console.error("Error fetching questions:", error));
    });

function getSurveyId() {
    return fetch('/Survey/GetChoosenSurvey')
        .then(res => res.json())
        .then(data => {
            if (data.surveyId > 0) {
                return data.surveyId;
            } else {
                return null;
            }
        });
}

function listAnswers(questionId, containerDiv, answerStats = {}) {
    fetch(`/Survey/ListAnswers?questionId=${questionId}`)
        .then(response => response.json())
        .then(answers => {

            let total = 0;
            for (const answerId in answerStats) {
                total += answerStats[answerId];
            }

            const table = document.createElement("table");
            table.classList.add("results-table");

            const thead = document.createElement("thead");
            thead.innerHTML = `
                <tr>
                    <th>Odpowiedź</th>
                    <th>Głosy</th>
                    <th>Procent</th>
                </tr>
            `;
            table.appendChild(thead);

            const tbody = document.createElement("tbody");

            answers.forEach(answer => {
                const count = answerStats[answer.id] || 0;
                const percent = total > 0 ? Math.round((count / total) * 100) : 0;

                const row = document.createElement("tr");
                row.innerHTML = `
                    <td>${answer.content}</td>
                    <td>${count}</td>
                    <td>${percent}%</td>
                `;

                tbody.appendChild(row);
            });

            table.appendChild(tbody);
            containerDiv.appendChild(table);
        })
        .catch(error => console.error("Error fetching answers:", error));
}


//function listAnswers(questionId, containerDiv, answerStats = {}) {
//    fetch(`/Survey/ListAnswers?questionId=${questionId}`)
//        .then(response => response.json())
//        .then(answers => {

//            let total = 0;
//            for (const answerId in answerStats) {
//                total += answerStats[answerId];
//            }

//            answers.forEach(answer => {

//                const paragraph = document.createElement("p");
//                paragraph.textContent = answer.content;

//                const strong = document.createElement("strong")

//                if ((answer.id in answerStats) && (total > 0)) {

//                    console.log("jest");
//                   strong.textContent = `    ${Math.round(answerStats[answer.id] / total * 100)}%`


//                }

//                else {
//                    strong.textContent = `    0%`
//                }

//                paragraph.appendChild(strong);
//                containerDiv.appendChild(paragraph);


//            });
//        })
//        .catch(error => console.error("Error fetching answers:", error));
//}

async function getStats(surveyId) {
    try {
        const response = await fetch(`/SurveyResults/GetStats?surveyId=${surveyId}`);
        const stats = await response.json();
        return stats;
    } catch (err) {
        console.error(err);
        return false;
    }
}
