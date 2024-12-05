package com.example.schedule

import androidx.compose.foundation.layout.*
import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.material3.*
import androidx.compose.runtime.*
import androidx.compose.ui.Modifier
import androidx.compose.ui.unit.dp
import kotlinx.coroutines.launch

@Composable
fun UserScreen() {
    val userService = ApiClient.retrofit.create(UserService::class.java)
    val scope = rememberCoroutineScope()

    var login by remember { mutableStateOf("") }
    var password by remember { mutableStateOf("") }
    var users by remember { mutableStateOf(listOf<User>()) }
    var message by remember { mutableStateOf("") }

    Column(modifier = Modifier.padding(16.dp)) {
        Text("Create User", style = MaterialTheme.typography.headlineSmall)
        Spacer(modifier = Modifier.height(8.dp))
        TextField(
            value = login,
            onValueChange = { login = it },
            label = { Text("Login") },
            modifier = Modifier.fillMaxWidth()
        )
        Spacer(modifier = Modifier.height(8.dp))
        TextField(
            value = password,
            onValueChange = { password = it },
            label = { Text("Password") },
            modifier = Modifier.fillMaxWidth()
        )
        Spacer(modifier = Modifier.height(16.dp))
        Button(onClick = {
            scope.launch {
                try {
                    userService.createUser(CreateUserRequest(login, password))
                    message = "User created successfully"
                    users = userService.getUsers()
                } catch (e: Exception) {
                    message = "Error: ${e.message}"
                }
            }
        }) {
            Text("Create User")
        }
        Spacer(modifier = Modifier.height(16.dp))
        Button(onClick = {
            scope.launch {
                try {
                    users = userService.getUsers()
                    message = "Users fetched successfully"
                } catch (e: Exception) {
                    message = "Error: ${e.message}"
                }
            }
        }) {
            Text("Get Users")
        }
        Spacer(modifier = Modifier.height(16.dp))

        Text(message, style = MaterialTheme.typography.bodyLarge)
        Spacer(modifier = Modifier.height(16.dp))
        LazyColumn {
            items(users.size) { index ->
                val user = users[index]
                Text("ID: ${user.id}, Login: ${user.login}, Token: ${user.token}")
                Spacer(modifier = Modifier.height(8.dp))
            }
        }
    }
}
