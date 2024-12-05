import React from "react";
import { useState } from 'react'
import './Dispatcher.scss'
import { FileDown } from "lucide-react";

function Dispatcher() {
  const [uploadStatus, setUploadStatus] = useState("");
  const [file, setFile] = useState(null);

  const handleFileChange = (event) => {
    setFile(event.target.files[0]);
  };
  const handleSubmit = async (event) => {
    event.preventDefault();

    if (!file) {
      setUploadStatus("Ошибка: выберите файл перед загрузкой.");
      return;
    }

    const formData = new FormData();
    formData.append("schedule", file);

    try {
      setUploadStatus("Загрузка...");
      const response = await axios.post("", formData, {
        headers: {
          "Content-Type": "multipart/form-data",
        },
      });
      setUploadStatus(`Успех: ${response.data.message}`);
      setFile(null); // Сбросить выбранный файл после успешной загрузки
      event.target.reset(); // Сбросить форму
    } catch (error) {
      setUploadStatus(
        `Ошибка: ${error.response?.data?.message || "Не удалось загрузить файл"}`
      );
    }
  };

  return (
    <div className="formCard">
      <form onSubmit={handleSubmit}>
        <div style={{ marginBottom: "10px" }}>
          <label htmlFor="schedule">Выберите файл расписания:</label>
          <input
            type="file"
            id="schedule"
            onChange={handleFileChange}
            accept=".xlsx, .xls, .csv"
            style={{ display: "block", marginTop: "5px" }}
          />
        </div>
        <button className="submitButton" type="submit">
          Загрузить <FileDown />
        </button>
      </form>
      {uploadStatus && (
        <p style={{ marginTop: "10px", color: uploadStatus.startsWith("Ошибка") ? "red" : "green" }}>
          {uploadStatus}
        </p>
      )}
    </div>
  );
}

export default Dispatcher;
