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

};

// EVENT HANDLERS
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

const resetViewUser = () => {
    const userView = document.getElementById("viewUserById");
    userView.innerHTML = "";
}

const showDeletedMsg = (deletedUserId) => {
    const msgBox = document.getElementById("viewUserById");
    msgBox.classList.add('text-danger');
    msgBox.innerHTML = "User " + deletedUserId + " was deleted!";

    setTimeout(() => {
        msgBox.innerHTML = "";
        msgBox.classList.remove('text-danger');
    }, 2000)
}

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