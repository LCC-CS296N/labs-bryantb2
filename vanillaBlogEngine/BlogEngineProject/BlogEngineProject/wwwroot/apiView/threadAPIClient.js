const THREAD_API = "https://localhost:44391/api/UserAPI";

// get all users
const fetchAllUsers = async () => {
    const response = await fetch(THREAD_API);
    const final = await response.json();
    console.log(final)
    return final;
};

// get user by id
const fetchUserById = async (userId) => {
    const response = await fetch(THREAD_API + "/" + userId)
    const final = await response.json();
    return final;
}

// add user
const addUser = async (userData) => {
    const response = await fetch(THREAD_API, {
        method: 'POST',
        body: JSON.stringify(userData),
        headers: {
            'Content-Type': 'application/json'
        }
    });
    const final = await response.json();
    return final;
};

// delete user by id
const deleteUserById = async (userId) => {
    const response = await fetch(THREAD_API + "/" + userId, {
        method: 'DELETE'
    });
    const final = await response.json();
    return final;
};

// update user by Id
const updateUserById = async (userData, userId) => {
    const response = await fetch(THREAD_API + "/" + userId, {
        method: 'PUT',
        body: JSON.stringify(userData),
        headers: {
            'Content-Type': 'application/json'
        }
    });
    const final = await response.json();
    return final;
};

// EVENT HANDLERS
const updateUserEvent = async (e) => {
    e.preventDefault()
    // get userId value
    // update user
    // display message
    // refresh table
    // refresh user dropdown
    const dropDown = document.getElementById("userDropdown");
    const selectedUserId = dropDown.options[dropDown.selectedIndex].value;
    const updatedEmail = document.getElementById("updateEmailInput").value;
    const updatedName = document.getElementById("updateUsernameInput").value;
    const updatedPassword = document.getElementById("updatePasswordInput").value;

    // validation
    let shouldDisplayMsg = false;
    let msgText = "";
    if (selectedUserId == "") {
        shouldDisplayMsg = true;
        msgText = "Selected user is invalid";
    }
    if (updatedEmail == "") {
        shouldDisplayMsg = true;
        msgText = "Email text is invalid";
    }
    if (updatedPassword == "") {
        shouldDisplayMsg = true;
        msgText = "Password text is invalid";
    }
    if (updatedName == "") {
        shouldDisplayMsg = true;
        msgText = "Username text is invalid";
    }
    // show msg or send put request
    if (!shouldDisplayMsg) {
        const userData = {
            Name: updatedName,
            Email: updatedEmail,
            Password: updatedPassword
        };
        const user = await updateUserById(userData, selectedUserId);
        if (user != null) {
            // rebuild table
            // clear form
            // show success
            const userList = await fetchAllUsers();
            buildUserTable(userList);
            buildUserDropdown(userList);
            resetUserdropdown();
            resetUpdateHTMlForm();
            showUpdateMsg();

        } else {
            showFailMsg("Add user failed");
        }
    } else {
        showUpdateFailMsg(msgText);
    }
}

const deleteUserEvent = async (e) => {
    console.log('delete event has fired');
    e.preventDefault();
    // get user value
    // delete user
    // get user list
    // refresh table
    const userId = e.target.value;
    const deletedUser = await deleteUserById(userId);
    const userList = await fetchAllUsers();
    buildUserTable(userList);
    showDeletedMsg(deletedUser.standardUserID);
}

const getUserEvent = async (e) => {
    e.preventDefault;
    // get user data
    // build HTML
    const userId = e.target.value;
    console.log('user id is:' + userId);
    const userData = await fetchUserById(userId);
    console.log('user data is:');
    console.log(userData);
    buildSingleUserHTML(userData);
};

const addUserEvent = async (e) => {
    e.preventDefault();
    // parse data and submit
    const emailVal = document.getElementById("emailInput").value;
    const userNameVal = document.getElementById("usernameInput").value;
    const passwordVal = document.getElementById("passwordInput").value;

    // validation
    let shouldDisplayMsg = false;
    let msgText = "";
    if (emailVal == "") {
        shouldDisplayMsg = true;
        msgText = "Email text is invalid";
    }
    if (passwordVal == "") {
        shouldDisplayMsg = true;
        msgText = "Password text is invalid";
    }
    if (userNameVal == "") {
        shouldDisplayMsg = true;
        msgText = "Username text is invalid";
    }
    // show msg or send post request
    if (!shouldDisplayMsg) {
        const userData = {
            Name: userNameVal,
            Email: emailVal,
            Password: passwordVal
        };
        const user = await addUser(userData);
        if (user != null) {
            // rebuild table
            // clear form
            // show success
            const userList = await fetchAllUsers();
            buildUserTable(userList);
            resetHTMLForm();
            showSuccessMsg();

        } else {
            showFailMsg("Add user failed");
        }
    } else {
        showFailMsg(msgText);
    }
}

// UI RENDERING METHODS
const buildUserDropdown = (userList) => {
    // creates dropdown options from userlist
    const userDropdown = document.getElementById("userDropdown");
    userList.forEach((user) => {
        const option = document.createElement('option');
        option.value = user.standardUserID;
        option.text = user.name;
        userDropdown.appendChild(option);
    });
}

const buildUserTable = (userList) => {
    // remember that we are using the standardUser model
    const tableBody = document.getElementById("userTableBody");
    const tableHTML = userList.map((user, index) =>
        `<tr>   
            <th scope="row">${index}</th>
            <td>${user.standardUserID}</td>
            <td>${user.name}</td>
            <td>${user.password}</td>
            <td>${user.dateJoined}</td>
            <td>
                <button onclick={getUserEvent(event)} value=${user.standardUserID} class="btn btn-danger">View User Data</button>
            </td>
         </tr>`);
    // join array to create massive HTML string and set table rows
    tableBody.innerHTML = tableHTML.join('');
}

const buildSingleUserHTML = (userObject) => {
    const userView = document.getElementById("viewUserById");
    const userViewHTML = (
        `<div class="text-center bg-secondary d-flex flex-column text-light">
            <div class="p-2">UserID: ${userObject.standardUserID}</div>
            <div class="p-2">Username: ${userObject.name}</div>
            <div class="p-2">Password: ${userObject.password}</div>
            <div class="p-2">Password: ${userObject.dateJoined}</div>
            <div class="p-2">
                <button onclick={deleteUserEvent(event)} value="${userObject.standardUserID}" class="btn btn-danger">Delete this User?</button>
            </div>
        </div>`
    );
    userView.innerHTML = userViewHTML;
}

const resetHTMLForm = () => {
    document.getElementById("emailInput").value = "";
    document.getElementById("usernameInput").value = "";
    document.getElementById("passwordInput").value = "";
}

const resetUpdateHTMlForm = () => {
    document.getElementById("updateEmailInput").value = "";
    document.getElementById("updateUsernameInput").value = "";
    document.getElementById("updatePasswordInput").value = "";
}

const resetViewUser = () => {
    const userView = document.getElementById("viewUserById");
    userView.innerHTML = "";
}

const resetUserdropdown = () => {
    const dropDown = document.getElementById("userDropdown");
    dropDown.selectedIndex = 0;
}

// update user msg
const showUpdateMsg = () => {
    const msgBox = document.getElementById("updateSuccessMessage");
    msgBox.classList.add('text-success');
    msgBox.innerHTML = "User was updated!";

    setTimeout(() => {
        msgBox.innerHTML = "";
        msgBox.classList.remove('text-success');
    }, 2000)
}

const showUpdateFailMsg = (msg) => {
    const msgBox = document.getElementById("updateSuccessMessage");
    msgBox.classList.add('text-danger');
    msgBox.innerHTML = msg;
}

// delete user msg
const showDeletedMsg = (deletedUserId) => {
    const msgBox = document.getElementById("viewUserById");
    msgBox.classList.add('text-danger');
    msgBox.innerHTML = "User " + deletedUserId + " was deleted!";

    setTimeout(() => {
        msgBox.innerHTML = "";
        msgBox.classList.remove('text-danger');
    }, 2000)
}

// add user messages
const showFailMsg = (msg) => {
    const msgBox = document.getElementById("successMessage");
    msgBox.classList.add('text-danger');
    msgBox.innerHTML = msg;
}

const showSuccessMsg = () => {
    const msgBox = document.getElementById("successMessage");
    msgBox.classList.add('text-success');
    msgBox.innerHTML = "User was added!";

    setTimeout(() => {
        msgBox.innerHTML = "";
        msgBox.classList.remove('text-success');
    }, 2000)
}