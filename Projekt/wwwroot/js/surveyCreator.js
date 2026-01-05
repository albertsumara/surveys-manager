const survey = {
    title: "",
    questions: []
};

const question = {

    content: "",
    answers: []

}


document.addEventListener("DOMContentLoaded", function () {
    const surveyTitleDiv = document.getElementById("survey-title");


    document.getElementById("setTitleBtn").addEventListener("click", () => {
        const titleInput = document.getElementById("surveyTitle");
        const title = titleInput.value.trim();

        //titleInput.textContent = "Dalej";

        if (!title) {
            alert("Title cannot be empty!");
            return;
        }

        if (title.length > 10) {
            alert("Title cannot exceed 10 characters!");
            return;
        }

        survey.title = titleInput.value;
        console.log(survey); 

        window.location.href = "/Survey/CreateSurveyQuestion";

    });


    document.getElementById("setQuestionContentBtn").addEventListener("click", () => {
        const questionContentInput = document.getElementById("surveyQuestionContent");
        const questionContent = questionContentInput.value.trim();

        //questionContentInput.textContent = "Dalej";

        if (!questionContent) {
            alert("Content of question cannot be empty!");
            return;
        }

        if (questionContent.length > 10) {
            alert("Content of question cannot exceed 10 characters!");
            return;
        }

        question.content = questionContentInput.value;
        questionContentInput.value = "";

        //console.log(survey);

        window.location.href = "/Survey/CreateSurveyAnswer";

    });

    document.getElementById("setAnswerContentBtn").addEventListener("click", () => {
        const answerContentInput = document.getElementById("surveyAnswerContent");
        const answerContent = answerContentInput.value.trim();

        //answerContentInput.textContent = "Dodaj odpowiedź";


        if (!answerContent) {
            alert("Content of answer cannot be empty!");
            return;
        }

        //if (Answer.length > 10) {
        //    alert("Content of question cannot exceed 10 characters!");
        //    return;
        //}

        question.answers.push(answerContentInput.value);

        answerContentInput.value = "";

        window.location.href = "/Survey/CreateSurveyAnswer";

    });


    document.getElementById("setQuestion").addEventListener("click", () => {
        const questionInput = document.getElementById("surveyQuestionAdd");

        //questionInput.textContent = "Dodaj następne pytanie";


        if (question.answers.length < 2) {
            alert("Question must have at least 2 answers!");
            //window.location.href = "/Survey/CreateSurveyAnswer";
            return;
        }

        survey.questions.push({ ...question });

        question.content = "";
        question.answers = [];

        window.location.href = "/Survey/CreateSurveyQuestion";

    });


//document.addEventListener("DOMContentLoaded", function () {
//    const questionListDiv = document.getElementById("question-list");

//    getSurveyId().then(surveyId => {
//        //console.log(surveyId);
//        if (!surveyId) return;
//        CurrentSurvey = surveyId;

//        fetch(`/SurveyResults/GetResults?surveyId=${surveyId}`)
//            .then(response => {
//                if (!response.ok) throw new Error("Network Error");
//                return response.json();
//            })
//            .then(questions => {

//                console.log(questions);

//            });

//        fetch(`/Survey/ListQuestions?surveyId=${surveyId}`)
//            .then(response => {
//                if (!response.ok) throw new Error("Network Error");
//                return response.json();
//            })
//            .then(questions => {
//                questionListDiv.innerHTML = "";

//                const answersForm = document.createElement("form");

//                const submitButton = document.createElement("button");

//                answersForm.id = "form";

//                submitButton.type = "submit";

//                submitButton.textContent = "Zatwierdź";

//                answersForm.addEventListener("submit", (event) => onSubmit(event, surveyId));

//                answersForm.setAttribute("id", "answer-picker");

//                questions.forEach(question => {
//                    //console.log(question);

//                    const questionDiv = document.createElement("div");
//                    const questionTitle = document.createElement("h4");
//                    questionTitle.textContent = question.content;
//                    questionDiv.appendChild(questionTitle);
//                    answersForm.appendChild(questionDiv);


//                    listAnswers(question.id, questionDiv);

//                    //    questionDiv.appendChild(document.createElement("br"));
//                });

//                answersForm.appendChild(submitButton);

//                questionListDiv.appendChild(answersForm);

//            })
//            .catch(error => console.error("Error fetching questions:", error));
//    });
//});

//function getSurveyId() {
//    return fetch('/Survey/GetChoosenSurvey')
//        .then(res => res.json())
//        .then(data => {
//            //console.log(data);
//            if (data.surveyId > 0) {
//                //console.log("Aktualna ankieta:", data.surveyId);
//                return data.surveyId;
//            } else {
//                return null;
//            }
//        });
//}

//function listAnswers(questionId, containerDiv) {
//    fetch(`/Survey/ListAnswers?questionId=${questionId}`)
//        .then(response => response.json())
//        .then(answers => {
//            //containerDiv.innerHTML = "";
//            //console.log(answers);
//            answers.forEach(answer => {
//                //console.log(answer);
//                const input = document.createElement("input");
//                const label = document.createElement("label");
//                const br = document.createElement("br");

//                input.setAttribute("type", "radio");
//                input.setAttribute("name", questionId);
//                input.setAttribute("value", answer.id);

//                //input.textContent = answer.content;

//                label.appendChild(input);
//                label.append(answer.content);

//                containerDiv.appendChild(label);
//                containerDiv.appendChild(br);
//            });
//        })
//        .catch(error => console.error("Error fetching answers:", error));
//}


//function onSubmit(event, surveyId) {

//    event.preventDefault();

//    const formData = new FormData(event.target);

//    console.dir(formData);

//    fetch(`/SurveyResults/Results`, {

//        method: "POST",

//        headers: {
//            'Content-Type': 'application/json',
//            'Accept': 'application/json'
//        },
//        body: JSON.stringify({

//            surveyId: surveyId,
//            ChoosenAnswers: Array.from(formData.entries().map(([key, value]) => ({ questionId: key, answerId: value })))
//        })


//    })

//    //console.log(event.target);





//}