import React from "react";
import { useState } from "react";
import { Link } from "react-router-dom";
import { CalendarDays, User, Bell, NotebookPen } from "lucide-react";
import {
  Drawer,
  Button,
  DialogTitle,
  Input,
  Sheet,
  Typography,
  Switch,
  Tooltip,
  Dropdown,
  MenuButton,
  Menu,
  MenuItem,
  colors,
  Divider,
} from "@mui/joy";
import "./Header.scss";

const Header = () => {
  const [open, setOpen] = React.useState(false);

  return (
    <>
      <div className="headerContainer">
        <Link className="header-link logo" to="/">
          <img src="" />
        </Link>

        <Tooltip
          className="header-tooltip"
          title="Расписание"
          enterDelay={600}
          leaveDelay={150}
        >
          <Link className="header-link muiBtn" to="/">
            <CalendarDays
              className="headerContainerIcon"
              color="#1d1e1d"
              strokeWidth={2.5}
              size={30}
            />
          </Link>
        </Tooltip>

        <div className="container-flex">
          <Tooltip
            className="header-tooltip"
            title="Изменения"
            enterDelay={600}
            leaveDelay={150}
          >
            <Link to="/changes" className="link">
              <NotebookPen
                className="headerContainerIcon"
                color="#1d1e1d"
                strokeWidth={2.5}
                size={30}
              />
            </Link>
          </Tooltip>
          <Tooltip
            className="header-tooltip"
            title="Уведомления"
            enterDelay={600}
            leaveDelay={150}
          >
            <Dropdown>
              <MenuButton className="muiBtn" variant="plain">
                <Bell
                  className="headerContainerIcon"
                  color="#1d1e1d"
                  strokeWidth={2.5}
                  size={30}
                />
              </MenuButton>
              <Menu>
                <MenuItem>Notification 1</MenuItem>
                <MenuItem>Notification 2</MenuItem>
                <MenuItem>Notification 3</MenuItem>
              </Menu>
            </Dropdown>
          </Tooltip>
          <Tooltip
            className="header-tooltip"
            title="Профиль"
            enterDelay={600}
            leaveDelay={150}
          >
            <Button className="muiBtn" onClick={() => setOpen(true)}>
              <User
                className="headerContainerIcon"
                color="#1d1e1d"
                strokeWidth={2.5}
                size={30}
              />
            </Button>
          </Tooltip>
        </div>
      </div>
      <Drawer
        className="profile-drawer"
        size="md"
        variant="plain"
        open={open}
        onClose={() => setOpen(false)}
        anchor="right"
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
          className="drawer-sheet"
          sx={{
            borderRadius: "md",
            p: 2,
            display: "flex",
            flexDirection: "column",
            gap: 2,
            height: "100%",
            overflow: "auto",
          }}
        >
          <div className="profile-drawer-div">
            <img className="profile-logo" src="./public/vite.svg" />
            <div className="profile-name-group-prep">
              <Typography level="body" sx={{ color: "#fefcfb" }}>
                Name
              </Typography>
              <Typography level="body" sx={{ color: "#fefcfb" }}>
                Group
              </Typography>
              <Typography level="body" sx={{ color: "#fefcfb" }}>
                Teacher
              </Typography>
            </div>
          </div>
          <Divider />
          <div
            style={{
              display: "inline-flex",
              alignItems: "center",
              gap: "10px",
              paddingLeft: "15px",
            }}
          >
            <Link to="/load" className="link" onClick={() => setOpen(false)}>
              <Typography sx={{ color: "#fefcfb" }}>
                Загрузить расписание
              </Typography>
            </Link>
          </div>
          <Divider />
          <div
            style={{
              display: "inline-flex",
              alignItems: "center",
              gap: "10px",
              paddingLeft: "15px",
            }}
          >
            <Typography sx={{ color: "#fefcfb" }}>Уведомления</Typography>
            <Switch />
          </div>
        </Sheet>
      </Drawer>
    </>
  );
};

export default Header;
