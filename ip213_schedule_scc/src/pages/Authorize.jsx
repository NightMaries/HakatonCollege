import React from "react";
import { Input } from "@mui/joy";
import "./Authorize.scss";
import { LogIn } from "lucide-react";
import axios from "axios";
import { Observer } from "mobx-react";
import userStore from "../../GlobalStore";


export default function Authorize() {
  const [formData, setFormData] = React.useState({
    login: "",
    password: ""
  });
  const [hasError, setHasError] = React.useState(false);
  const [errorMsg, setErrorMsg] = React.useState('');

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      const response = await axios.get(
        `https://localhost:5058/User/Login?Login=${formData.login}&Password=${formData.password}`
      );

      userStore.userID = response.data.id;
      userStore.userRole = response.data.roleId;
      
    } catch (error) {
      switch (error.response?.status) {
        case 500:
          setErrorMsg("пользователь не существует.");
          break;
        case 400:
          setErrorMsg("нет ответа от сервера.");
          break;
        default: 
          setErrorMsg("неизвестно.");
      }
      setHasError(true);
    }
  };

  return (
    <>
      <div className="formContainer">
        <form className="form" onSubmit={handleSubmit}>
          <div className="inputGroup">
            <Input
              className="formInput"
              placeholder="Логин"
              variant="soft"
              type="text"
              value={formData.login}
              onChange={(e) =>
                setFormData({ ...formData, login: e.target.value })
              }
            />
            <Input
              className="formInput"
              placeholder="Пароль"
              variant="soft"
              type="text"
              value={formData.password}
              onChange={(e) =>
                setFormData({ ...formData, password: e.target.value })
              }
            />
          </div>
          <p className={hasError ? "errMsg" : "errMsg disabled" }>
            Ошибка, {errorMsg}
          </p>
          <button className="formSubmit" type="submit">
            Войти <LogIn />
          </button>
        </form>
      </div>
    </>
  );
}
