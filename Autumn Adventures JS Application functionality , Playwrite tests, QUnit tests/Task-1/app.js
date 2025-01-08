window.addEventListener('load', solve);

function solve() {

    //initial map of elements
    const timeElement = document.getElementById('time');
    const dateElement = document.getElementById('date');
    const placeElement = document.getElementById('place');
    const eventElement = document.getElementById('event-name');
    const emailElement = document.getElementById('email');
    const addButtonElement = document.getElementById('add-btn');
    const checkListElement = document.getElementById('check-list');
    const upcomingListElement = document.getElementById('upcoming-list');
    const finishedListElement = document.getElementById('finished-list');
    const clearButtonElement = document.getElementById('clear');

    //add event listener for clicking addEvent button
    addButtonElement.addEventListener('click', onNext);

    function onNext(e) {
        e.preventDefault();

        //check if all fields are filled
        if (timeElement.value == '' || dateElement.value == '' || placeElement.value == '' ||
            eventElement.value == '' || emailElement.value == ''
        ) {
            return;
        }

        //building li element with all children inside
        let liElement = document.createElement('li');
        liElement.setAttribute('class', 'event-content');

        let articleElement = document.createElement('article');

        let timeParagraph = document.createElement('p');
        timeParagraph.textContent = `Begins: ${dateElement.value} at: ${timeElement.value}`;

        let placeParagraph = document.createElement('p');
        placeParagraph.textContent = `In: ${placeElement.value}`;

        let eventParagraph = document.createElement('p');
        eventParagraph.textContent = `Event: ${eventElement.value}`;

        let emailParagraph = document.createElement('p');
        emailParagraph.textContent = `Contact: ${emailElement.value}`;

        let editBtn = document.createElement('button');
        editBtn.setAttribute('class', 'edit-btn');
        editBtn.textContent = "Edit";

        let continueBtn = document.createElement('button');
        continueBtn.setAttribute('class', 'continue-btn');
        continueBtn.textContent = "Continue";

        articleElement.appendChild(timeParagraph);
        articleElement.appendChild(placeParagraph);
        articleElement.appendChild(eventParagraph);
        articleElement.appendChild(emailParagraph);

        liElement.appendChild(articleElement);
        liElement.appendChild(editBtn);
        liElement.appendChild(continueBtn);

        checkListElement.appendChild(liElement);

        addButtonElement.disabled = true;

        //save the data in variables so we wont lose it

        let editedTimeElement = timeElement.value;
        let editedDateElement = dateElement.value;
        let editedPlaceElement = placeElement.value;
        let editedEventElement = eventElement.value;
        let editedEmailElement = emailElement.value;

        //clear all inputs
        timeElement.value = '';
        dateElement.value = '';
        placeElement.value = '';
        eventElement.value = '';
        emailElement.value = '';

        //logic for edit button
        editBtn.addEventListener('click', onEdit);

        function onEdit() {
            timeElement.value = editedTimeElement;
            dateElement.value = editedDateElement;
            placeElement.value = editedPlaceElement;
            eventElement.value = editedEventElement;
            emailElement.value = editedEmailElement;

            liElement.remove();
            addButtonElement.disabled = false;
        }

        //logic for continue button
        continueBtn.addEventListener('click', onContinue);

        function onContinue(){
            let liElementContinue = document.createElement('li');
            liElementContinue.setAttribute('class', 'event-content');

            let articleElementContinue = document.createElement('article');
            articleElementContinue = articleElement;

            let moveToFinishElement = document.createElement('button');
            moveToFinishElement.setAttribute('class', 'finished-btn');
            moveToFinishElement.textContent = 'Move to Finished';

            liElementContinue.appendChild(articleElementContinue);
            liElementContinue.appendChild(moveToFinishElement);
            upcomingListElement.appendChild(liElementContinue);

            liElement.remove();
            addButtonElement.disabled = false;


            //logic for move to finish button
            moveToFinishElement.addEventListener('click', moveToFinish);

            function moveToFinish(){
                let liElementFinished = document.createElement('li');
                liElementFinished.setAttribute('class', 'event-content');

                let articleElementFinished = document.createElement('article');
                articleElementFinished = articleElementContinue;

                liElementFinished.appendChild(articleElementFinished);
                finishedListElement.appendChild(liElementFinished);
                liElementContinue.remove();

                //add logic for clear button
                clearButtonElement.addEventListener('click', onClear);

                function onClear(){
                    liElementFinished.remove();
                }
            }
        }
    }
}




