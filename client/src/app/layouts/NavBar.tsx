import { Group } from "@mui/icons-material";
import {
  AppBar,
  Box,
  Toolbar,
  Typography,
  Container,
  MenuItem,
  Button,
  MenuList,
} from "@mui/material";

type NavBarProps = {
  openForm: () => void;
};

export default function NavBar({ openForm }: NavBarProps) {
  return (
    <Box sx={{ flexGrow: 1 }}>
      <AppBar
        position="static"
        sx={{
          backgroundImage:
            "linear-gradient(135deg, #182a73 0%, #218aae 69%, #20a7ac 89%)",
        }}
      >
        <Container maxWidth="xl">
          <Toolbar sx={{ display: "flex", justifyContent: "space-between" }}>
            <Box
              sx={{
                display: "flex",
                alignItems: "center",
                gap: 2,
              }}
            >
              <Group fontSize="large" />
              <Typography variant="h4" sx={{ cursor: "pointer" }}>
                Reactivities
              </Typography>
            </Box>
            <MenuList sx={{ display: "flex" }}>
              <MenuItem
                sx={{
                  fontSize: "1.2rem",
                  textTransform: "uppercase",
                  fontWeight: "bold",
                }}
              >
                Activities
              </MenuItem>
              <MenuItem
                sx={{
                  fontSize: "1.2rem",
                  textTransform: "uppercase",
                  fontWeight: "bold",
                }}
              >
                About
              </MenuItem>
              <MenuItem
                sx={{
                  fontSize: "1.2rem",
                  textTransform: "uppercase",
                  fontWeight: "bold",
                }}
              >
                Contact
              </MenuItem>
            </MenuList>
            <Button
              size="large"
              variant="contained"
              color="warning"
              onClick={openForm}
            >
              Create activity
            </Button>
          </Toolbar>
        </Container>
      </AppBar>
    </Box>
  );
}
