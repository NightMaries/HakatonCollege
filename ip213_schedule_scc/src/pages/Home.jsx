import React, { useState } from "react";
import "./Home.scss";
import {
  Table,
  CardContent,
  Typography,
  Card,
  Divider,
  Drawer,
  Sheet,
  Button,
  IconButton,
  Select,
  Option,
  Autocomplete,
  Switch,
  Input,
} from "@mui/joy";
import { CalendarDays, Settings, Table2, X } from "lucide-react";

const Home = () => {
  const [open, setOpen] = React.useState(false);
  const [value, setValue] = React.useState("grp");
  const [sortWay, setSortWay] = React.useState("grp");
  const action = React.useRef(null);
  const [selectedGroups, setSelectedGroups] = useState([]);
  

  const shedules = [
    {
      id: 1,
      groupName: "ИП213",
      subjectName: "МДК 02.03",
      teacherFIO: "Николаенко Ксения Сергеевна",
      weekDay: "Понедельник",
      studyWeekId: 1,
      scheludeNumber: 2,
      scheduleStart: "00:00:00",
      scheduleEnd: "00:00:00",
      currentPair: true
    },
    {
      id: 1,
      groupName: "ИП211",
      subjectName: "МДК 02.03",
      teacherFIO: "Николаенко Ксения Сергеевна",
      weekDay: "Понедельник",
      studyWeekId: 1,
      scheludeNumber: 3,
      scheduleStart: "00:00:00",
      scheduleEnd: "00:00:00",
      currentPair: false
    },
    {
      id: 2,
      groupName: "ИП214",
      subjectName: "МДК 02.03",
      teacherFIO: "Николаенко Ксения Сергеевна",
      weekDay: "Вторник",
      studyWeekId: 1,
      scheludeNumber: 1,
      scheduleStart: "00:00:00",
      scheduleEnd: "00:00:00",
      currentPair: false
    },
    {
      id: 3,
      groupName: "ИП214",
      subjectName: "МДК 02.03",
      teacherFIO: "Николаенко Ксения Сергеевна",
      weekDay: "Вторник",
      studyWeekId: 1,
      scheludeNumber: 5,
      scheduleStart: "00:00:00",
      scheduleEnd: "00:00:00",
      currentPair: false
    },
    {
      id: 5,
      groupName: "ИП213",
      subjectName: "МДК 02.03",
      teacherFIO: "Николаенко Ксения Сергеевна",
      weekDay: "Вторник",
      studyWeekId: 1,
      scheludeNumber: 5,
      scheduleStart: "00:00:00",
      scheduleEnd: "00:00:00",
      currentPair: false
    },
    {
      id: 5,
      groupName: "ИП216",
      subjectName: "МДК 02.03",
      teacherFIO: "Николаенко Ксения Сергеевна",
      weekDay: "Четверг",
      studyWeekId: 1,
      scheludeNumber: 5,
      scheduleStart: "00:00:00",
      scheduleEnd: "00:00:00",
      currentPair: false
    },
  ];
  const initialGroupNames = Array.from(
    new Set(shedules.map((item) => item.groupName))
  ).map((name) => ({ title: name }));

  let groupNames;
  selectedGroups.length !== 0
    ? (groupNames = initialGroupNames.filter((group) =>
        selectedGroups.some((filterGroup) => filterGroup.title === group.title)
      ))
    : (groupNames = initialGroupNames);

  const prepodNames = Array.from(
    new Set(shedules.map((item) => item.teacherFIO))
  ).map((name) => ({ title: name }));

  // const weekDays = [...new Set(shedules.map((item) => item.weekDay))];
  const weekDays = ["Понедельник", "Вторник", "Среда", "Четверг", "Пятница"];

  const [searchTerm, setSearchTerm] = useState("");

  return (
    <>
      <Card className="scheduleContainer" variant="soft">
        <div className="cardHeader">
          <button className="settings" onClick={() => setOpen(true)}>
            <Settings
              className="settingsIcon"
              style={{ color: "#fefcfb" }}
              size={30}
            />
          </button>
          <Input
            className="searchInput"
            placeholder="Искать..."
            variant="soft"
            value={searchTerm}
            onChange={(e) => setSearchTerm(e.target.value)}
          />
        </div>
        <CardContent className="scheduleContainerFlex">
          <Table
            borderAxis="bothBetween"
            className="fullSchedule"
            sx={{ width: `${(sortWay === 'grp' ? groupNames : prepodNames ).length * 320 + 100}px` }}
          >
            <thead>
              <tr>
                <th style={{ width: "50px" }}></th>
                <th style={{ width: "50px" }}></th>
                {(sortWay === "grp"
                  ? groupNames : prepodNames).map((subj) => (
                      <th
                        key={subj.title}
                        className="groupScheduleTitle"
                        style={{ width: "340px" }}
                      >
                        <Typography className="titleTextCenter">
                          {subj.title}
                        </Typography>
                      </th>
                    ))}
              </tr>
            </thead>

            <tbody>
              {weekDays.map((day) => (
                <>
                  <tr key={day}>
                    <td className="weekDay" rowSpan={7}>
                      {day}
                    </td>
                  </tr>
                  {[1, 2, 3, 4, 5, 6].map((lesson) => (
                    <tr key={`${day}-${lesson}` }>
                      {!shedules.some(subj =>  subj.currentPair && subj.scheludeNumber === lesson) ? <td>{lesson}</td> : <td className="">{lesson}</td>}
                      

                      {(sortWay === "grp" ? groupNames : prepodNames).map((item) => {
              let schedule = shedules.find(
                (s) =>
                  (sortWay === "grp"
                    ? s.groupName === item.title
                    : s.teacherFIO === item.title) &&
                  s.weekDay === day &&
                  s.scheludeNumber === lesson
              );
              return (
                <td key={`${day}-${lesson}-${item.title}`}>
                  {schedule ? (
                    <Card variant="plain" className="subjectCard">
                      <CardContent>
                        <Typography className="titleText">
                          <span
                            className={
                              schedule.subjectName
                                .toLowerCase()
                                .includes(searchTerm.toLowerCase()) &&
                              searchTerm !== ""
                                ? "find"
                                : ""
                            }
                          >
                            {schedule.subjectName}
                          </span>
                        </Typography>
                        <div className="subjectCardDescription">
                          <Typography className="subjectCardDescriptionText">
                            {schedule.scheduleStart} - {schedule.scheduleEnd}
                          </Typography>
                          <Typography className="subjectCardDescriptionText">
                            <span
                              className={
                                (value === "grp"
                                  ? schedule.teacherFIO
                                  : schedule.groupName)
                                    .toLowerCase()
                                    .includes(searchTerm.toLowerCase()) &&
                                searchTerm !== ""
                                  ? "find"
                                  : ""
                              }
                            >
                              {sortWay === "grp"
                                ? schedule.teacherFIO
                                : schedule.groupName}
                            </span>
                          </Typography>
                        </div>
                      </CardContent>
                    </Card>
                  ) : (
                    "-"
                  )}
                          </td>
                        );
                      })}
                    </tr>
                  ))}
                  <Divider
                    className="divider"
                    sx={{ width: `${(sortWay === 'grp' ? groupNames : prepodNames ).length * 360 + 130}px` }}
                  />
                </>
              ))}
            </tbody>
          </Table>
        </CardContent>
      </Card>

      <Drawer
        size="md"
        variant="plain"
        open={open}
        onClose={() => setOpen(false)}
        slotProps={{
          content: {
            sx: {
              bgcolor: "transparent",
              p: { md: 3, sm: 0 },
              boxShadow: "none",
            },
          },
        }}
      >
        <Sheet
          className="drawerSheet"
          sx={{
            borderRadius: "md",
            display: "flex",
            flexDirection: "column",
            gap: 2,
            height: "100%",
            overflow: "auto",
            bgcolor: "#415a77",
          }}
        >
          <Typography sx={{ color: "#fefcfb" }}>
            Настройки отображения
          </Typography>
          <Divider className="dividerText">Отображение</Divider>
          <div className="btnGroup">
            <Table2 size={32} color="#fefcfb" />
            <Switch size="lg" color="neutral" />
            <CalendarDays size={32} color="#fefcfb" />
          </div>

          <Divider className="dividerText">Сортировка</Divider>
          <Select
            className="selection"
            action={action}
            value={value}
            placeholder="Сортировать по..."
            onChange={(event, newValue) => {setValue(newValue); setSortWay(newValue)}}
            {...(value && {
              endDecorator: (
                <IconButton
                  size="sm"
                  variant="plain"
                  color="neutral"
                  onMouseDown={(event) => {
                    event.stopPropagation();
                  }}
                  onClick={() => {
                    setValue(null);
                    setSortWay('grp');
                    action.current?.focusVisible();
                  }}
                >
                  <X />
                </IconButton>
              ),
              indicator: null,
            })}
            sx={{ minWidth: 160 }}
          >
            <Option value="grp">Группе</Option>
            <Option value="tch">Преподователю</Option>
          </Select>

          <Divider className="dividerText">Сравнение</Divider>
          <Autocomplete
            className="selection"
            multiple
            id="tags-default"
            placeholder="Добавить группу..."
            options={initialGroupNames}
            onChange={(event, value) => setSelectedGroups(value)}
            getOptionLabel={(option) => option.title}
            disabled={value === 'tch'}
          />
        </Sheet>
      </Drawer>
    </>
  );
};

export default Home;
