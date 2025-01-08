window.addEventListener('load', solve);

function solve() {
        //initial map of elements
        const carModelElement = document.getElementById('car-model');
        const carYearElement = document.getElementById('car-year');
        const partNameElement = document.getElementById('part-name');
        const partNumberElement = document.getElementById('part-number');
        const conditionElement = document.getElementById('condition');
        const nextBtnElement = document.getElementById('next-btn');

        const infoListElement = document.querySelector('.info-list');
        const confirmOrderElement = document.querySelector('.confirm-list');

        //add event listener for clicking addEvent button
        nextBtnElement.addEventListener('click', onNext);

        function onNext(e) {
                e.preventDefault();

                //check if all fields are filled
                if (carModelElement.value == '' ||
                        carYearElement.value == '' ||
                        partNameElement.value == '' ||
                        partNumberElement.value == '' ||
                        conditionElement == '') {
                        return;
                }

                //building li element with all children inside
                let liElement = document.createElement('li');
                liElement.setAttribute('class', 'part-content');

                let articleElement = document.createElement('article');

                let carModelParagraph = document.createElement('p');
                carModelParagraph.textContent = `Car Model: ${carModelElement.value}`;

                let carYearParagraph = document.createElement('p');
                carYearParagraph.textContent = `Car Year: ${carYearElement.value}`;

                let partNameParagraph = document.createElement('p');
                partNameParagraph.textContent = `Part Name: ${partNameElement.value}`;

                let partNumberParagraph = document.createElement('p');
                partNumberParagraph.textContent = `Part Number: ${partNumberElement.value}`;

                let conditionParagraph = document.createElement('p');
                conditionParagraph.textContent = `Condition: ${conditionElement.value}`;

                let editBtn = document.createElement('button');
                editBtn.setAttribute('class', 'edit-btn');
                editBtn.textContent = "Edit";

                let continueBtn = document.createElement('button');
                continueBtn.setAttribute('class', 'continue-btn');
                continueBtn.textContent = "Continue";

                articleElement.appendChild(carModelParagraph);
                articleElement.appendChild(carYearParagraph);
                articleElement.appendChild(partNameParagraph);
                articleElement.appendChild(partNumberParagraph);
                articleElement.appendChild(conditionParagraph);

                liElement.appendChild(articleElement);
                liElement.appendChild(editBtn);
                liElement.appendChild(continueBtn);

                infoListElement.appendChild(liElement);

                nextBtnElement.disabled = true;

                //save the data in variables so we wont lose it
                let editedCarModelElement = carModelElement.value;
                let editedCarYearElement = carYearElement.value;
                let editedPartNameElement = partNameElement.value;
                let editedPartNumberElement = partNumberElement.value;
                let editedConditionElement = conditionElement.value;

                //clear all inputs
                carModelElement.value = '';
                carYearElement.value = '';
                partNameElement.value = '';
                partNumberElement.value = '';
                conditionElement.value = '';

                //logic for edit button
                editBtn.addEventListener('click', onEdit);

                function onEdit() {
                        carModelElement.value = editedCarModelElement;
                        carYearElement.value = editedCarYearElement;
                        partNameElement.value = editedPartNameElement;
                        partNumberElement.value = editedPartNumberElement;
                        conditionElement.value = editedConditionElement;

                        liElement.remove();
                        nextBtnElement.disabled = false;
                }

                //logic for continue button
                continueBtn.addEventListener('click', onContinue);

                function onContinue() {
                        let liElementContinue = document.createElement('li');
                        liElementContinue.setAttribute('class', 'part-content');

                        // Clone the article element to avoid moving the original one
                        let articleElementContinue = articleElement.cloneNode(true);

                        let confirmBtnElement = document.createElement('button');
                        confirmBtnElement.setAttribute('class', 'confirm-btn');
                        confirmBtnElement.textContent = 'Confirm';

                        let cancelBtnElement = document.createElement('button');
                        cancelBtnElement.setAttribute('class', 'cancel-btn');
                        cancelBtnElement.textContent = 'Cancel';

                        liElementContinue.appendChild(articleElementContinue);
                        liElementContinue.appendChild(confirmBtnElement);
                        liElementContinue.appendChild(cancelBtnElement);
                        confirmOrderElement.appendChild(liElementContinue);

                        liElement.remove();
                        nextBtnElement.disabled = false;

                        // Logic for Confirm button
                        confirmBtnElement.addEventListener('click', onConfirm);

                        function onConfirm() {
                                liElementContinue.remove();
                                document.getElementById('complete-img').style.visibility = 'visible';
                                document.getElementById('complete-text').textContent = "Part is Ordered!";
                        }

                        // Logic for Cancel button
                        cancelBtnElement.addEventListener('click', onCancel);

                        function onCancel() {
                                liElementContinue.remove();
                                nextBtnElement.disabled = false;
                        }
                }
        }
};




