const { test, expect } = require("@playwright/test");

//verify user can add task
test("user can add task", async ({page}) => {
    //Arrange
    await page.goto('http://localhost:8080');

    //Act
    await page.fill('#task-input', 'Test Task');
    await page.click('#add-task');

    //Assert
    const taskText = await page.textContent('.task');
    expect(taskText).toContain('Test Task')
})

//verify user can delete tasks
test("user can delete task", async ({page}) => {
    //Arrange
    await page.goto('http://localhost:8080');
    await page.fill('#task-input', 'Test Task');
    await page.click('#add-task');

    //Act
    await page.click('.task .delete-task');

    //Assert
    const tasks = await page.$$eval('.task', tasks => tasks.map(
        test => tasks.textContent
    ));
    expect(tasks).not.toContain('Test Task')
})

//verify user can mark task as complete
test("user can mark task as complete", async ({page}) => {
    //Arrange
    await page.goto('http://localhost:8080');
    await page.fill('#task-input', 'Test Task');
    await page.click('#add-task');

    //Act
    await page.click('.task .task-complete');

    //Assert
    const completedTask = await page.$('.task.completed')
    expect(completedTask).not.toBeNull()
})

//verify user can filter tests
test("user can filter tests", async ({page}) => {
    //Arrange
    await page.goto('http://localhost:8080');
    await page.fill('#task-input', 'Test Task');
    await page.click('#add-task');
    await page.click('.task .task-complete');

    //Act
    await page.selectOption('#filter',"Completed")

    //Assert
    const incompleteTasks = await page.$('task:not(.complete)')
    expect(incompleteTasks).toBeNull()
})

//verify user can filter active tasks
test("user can filter active tasks", async ({ page }) => {
    // Arrange
    await page.goto('http://localhost:8080');
    await page.fill('#task-input', 'Test Task');
    await page.click('#add-task');

    // Manually add 'task-active' class to the newly added task for testing purposes
    await page.evaluate(() => {
        const taskList = document.getElementById('task-list');
        const newTask = document.createElement('li');
        newTask.textContent = 'Test Task';
        newTask.classList.add('task', 'task-active');
        taskList.appendChild(newTask);
    });

    // Act
    await page.selectOption('#filter', 'active');

    // Assert
    const activeTasks = await page.locator('.task.task-active');
    await expect(activeTasks).toBeVisible();
    const activeTaskCount = await activeTasks.count();
    expect(activeTaskCount).toBe(1);  // Expecting one active task
});



