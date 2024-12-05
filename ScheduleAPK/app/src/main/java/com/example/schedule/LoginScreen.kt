package com.example.schedule

import android.widget.Toast
import androidx.compose.foundation.layout.*
import androidx.compose.foundation.text.KeyboardOptions
import androidx.compose.material3.*
import androidx.compose.runtime.*
import androidx.compose.ui.Modifier
import androidx.compose.ui.platform.LocalContext
import androidx.compose.ui.text.input.ImeAction
import androidx.compose.ui.text.input.PasswordVisualTransformation
import androidx.compose.ui.unit.dp
import kotlinx.coroutines.launch
import retrofit2.Response

@Composable
fun LoginScreen(onLoginSuccess: (String, String) -> Unit) {
    val username = remember { mutableStateOf("") }
    val password = remember { mutableStateOf("") }
    val context = LocalContext.current

    val scope = rememberCoroutineScope()

    Column(modifier = Modifier.padding(16.dp)) {
        Text("Вход", style = MaterialTheme.typography.labelSmall)

        TextField(
            value = username.value,
            onValueChange = { username.value = it },
            label = { Text("Логин") },
            modifier = Modifier.fillMaxWidth(),
            keyboardOptions = KeyboardOptions.Default.copy(
                imeAction = ImeAction.Next
            )
        )

        Spacer(modifier = Modifier.height(8.dp))

        TextField(
            value = password.value,
            onValueChange = { password.value = it },
            label = { Text("Пароль") },
            modifier = Modifier.fillMaxWidth(),
            visualTransformation = PasswordVisualTransformation(),
            keyboardOptions = KeyboardOptions.Default.copy(
                imeAction = ImeAction.Done
            )
        )

        Spacer(modifier = Modifier.height(16.dp))

        Button(
            onClick = {
                scope.launch {
                    try {
                        val loginResponse: Response<JsonResponse> = ApiClient.userService.loginUser(
                            login = username.value,
                            password = password.value
                        )
                        if (loginResponse.isSuccessful) {
                            val token = loginResponse.body()
                            val roleId = loginResponse.body()
                            if (token != null) {
                                if (roleId != null) {
                                    onLoginSuccess(token.toString(), roleId.toString())
                                }
                            } else {
                                Toast.makeText(context, "Не удалось получить токен", Toast.LENGTH_SHORT).show()
                            }
                        } else {
                            Toast.makeText(context, "Неверный логин или пароль", Toast.LENGTH_SHORT).show()
                        }
                    } catch (e: Exception) {
                        Toast.makeText(context, "Ошибка: ${e.message}", Toast.LENGTH_SHORT).show()
                    }
                }
            },
            modifier = Modifier.fillMaxWidth()
        ) {
            Text("Войти")
        }
    }
}




