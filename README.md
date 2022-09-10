# **ECAP-TestTask - Input Mirror Application**

### **Prerequisities**

- **CMD/PowerShell**
- **Docker**

---

## **How to run?**

1.  Download "compose-server-client.yaml" file
2.  Run a console/terminal
3.  Go to the directory where you downloaded the compose file
4.  Run this line **docker compose -f compose-server-client.yaml create**
5.  Open 2 more console/terminal tabs
6.  Put both console/terminal tabs side by side.
7.  Run **docker ps -a** to list all containers
8.  Copy the server and the client container id
9.  Run **docker start (_paste server container id here without parantheses_) -i**
10. Then run **docker start (_paste client container id here without parantheses_) -i**
11. Let the magic happens.

---

## **How to use**

### Just type whatever you want, server will mirror it.

### Remember! The text you see on both the client and the server is written by the server, client input is hidden.
