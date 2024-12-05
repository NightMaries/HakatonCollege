import React from "react";
import { Card, Table } from "@mui/joy";
import "./ChangesTable.scss";

const ChangesTable = ({ changes }) => {
  return (
    <Card className="changesScheduleConatiner">
      <Table borderAxis="bothBetween" className="changesSchedule">
        <thead>
          <tr>
            <th rowSpan={2}>Группа</th>
            <th colSpan={2}>Снимается по расписанию</th>
            <th colSpan={2}>Вводится в расписание</th>
            <th rowSpan={2}>Номер пары</th>
            <th rowSpan={2}>Причина</th>
            <th rowSpan={2}>Дата</th>
          </tr>
          <tr>
            <th>Предмет</th>
            <th>Преподаватель</th>
            <th>Предмет</th>
            <th>Преподаватель</th>
          </tr>
        </thead>
        <tbody>
          {changes.map((change) => (
            <tr key={change.id}>
              <td>{change.groupName}</td>
              <td>{change.oldSubjectName}</td>
              <td>{change.newSubjectName}</td>
              <td>{change.oldTeacherFIO}</td>
              <td>{change.newTeacherFIO}</td>
              <td>{change.pairNumber}</td>
              <td>{change.reason}</td>
              <td>{new Date(change.date).toLocaleString()}</td>
            </tr>
          ))}
        </tbody>
      </Table>
    </Card>
  );
};

export default ChangesTable;
