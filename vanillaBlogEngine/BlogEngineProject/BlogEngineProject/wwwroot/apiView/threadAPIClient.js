const THREAD_API = "https://localhost:44391/api/UserAPI";

// get all users
//const fetchAllUsers = async () => {
const fetchAllUsers = async () => {
    const response = await fetch(THREAD_API);
    const final = await response.json();
    console.log(final)
    return final;
};

// add user
const addUser = async (userData) => {

};

// delete user by id
const deleteUserById = async (userId) => {

};

// update user by Id
const updateUserById = async (userData, userId) => {

};

// UI METHODS
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
         </tr>`);
    // join array to create massive HTML string and set table rows
    tableBody.innerHTML = tableHTML.join('');
}