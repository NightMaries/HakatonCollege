import React from 'react'
import './AdminPanel.scss'
import { Button, Card, CardContent, Input, Table } from '@mui/joy'
import { CirclePlus, Pencil, Settings } from 'lucide-react'

function AdminPanel() {
    const [searchTerm, setSearchTerm] = React.useState("");

    const users = [
        {
            id: "1",
            login: "test1",
            role: "1",
        },
        {
            id: "2",
            login: "test2",
            role: "2",
        },
    ]

    return (
        <>
            <Card className="scheduleContainer" variant="soft">
                <div className="cardHeader">
                    <button className="addUser" onClick={() => setOpen(true)}>
                        <CirclePlus 
                            className="addUserIcon"
                            style={{ color: "#fefcfb" }}
                            size={30} />
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

                    >
                        <thead>
                            <tr>
                                <th style={{ width: "50px" }}>ID</th>
                                <th style={{ width: "150px" }}>Логин</th>
                                <th style={{ width: "100px" }}>Роль</th>
                                <th style={{ width: "50px" }}></th>
                            </tr>
                        </thead>

                        <tbody>
                            {users.map((user, index) => (
                                <tr key={index}>
                                    <td>{user.id}</td>
                                    <td>{user.login}</td>
                                    <td>{user.role}</td>
                                    <td><Button className="editButton"><Pencil /></Button></td>
                                </tr>
                            ))}
                        </tbody>
                    </Table>
                </CardContent>
            </Card>
        </>
    )
}

export default AdminPanel