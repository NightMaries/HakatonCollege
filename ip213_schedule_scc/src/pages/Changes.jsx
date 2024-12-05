import React from "react"
import ChangesTable from "../components/ChangesTable";

const Changes = () => {
    const changedShedule = [
        {
            id: 3,
            groupId: 6,
            oldSubjectId: 8,
            newSubjectId: 9,
            oldTeacherId: 8,
            newTeacherId: 9,
            groupName: "ИП213",
            oldSubjectName: "string",
            newSubjectName: "string",
            oldTeacherFIO: "string",
            newTeacherFIO: "string",
            pairNumber: 2,
            reason: "string",
            date: "2024-12-04T16:41:37.684Z"
          },
          {
            id: 4,
            groupId: 6,
            oldSubjectId: 10,
            newSubjectId: 11,
            oldTeacherId: 10,
            newTeacherId: 11,
            groupName: "ИП213",
            oldSubjectName: "Web-программирование",
            newSubjectName: "МДК 02.03",
            oldTeacherFIO: "string",
            newTeacherFIO: "string",
            pairNumber: 2,
            reason: "string",
            date: "2024-12-04T17:04:05.484Z"
          }
    ]
    return (
    <ChangesTable changes={changedShedule}/>
  )
};

export default Changes;
