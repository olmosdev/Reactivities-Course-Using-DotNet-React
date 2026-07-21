import {
  Card,
  Chip,
  Button,
  Typography,
  CardContent,
  CardActions,
  CardHeader,
  Box,
  Avatar,
} from "@mui/material";
import { AccessTime, Place } from "@mui/icons-material";
import Divider from "@mui/material/Divider";
import { Link } from "react-router";
import { formatDate } from "../../../lib/util/util";

type ActivityCardProps = {
  activity: Activity;
};

export default function ActivityCard({ activity }: ActivityCardProps) {
  const isHost = false;
  const isGoing = false;
  const label = isHost ? "You are hosting" : "You are going";
  const isCancelled = false;
  const color = isHost ? "secondary" : isGoing ? "warning" : "default";

  return (
    <Card elevation={3} sx={{ borderRadius: 3 }}>
      <Box
        sx={{
          display: "flex",
          justifyContent: "space-between",
          alignItems: "center",
          gap: 2,
        }}
      >
        <CardHeader
          sx={{ flex: 1, minWidth: 0 }}
          avatar={<Avatar sx={{ height: 80, width: 80 }} />}
          title={
            <Typography sx={{ fontWeight: "bold", fontSize: 20 }}>
              {activity.title}
            </Typography>
          }
          subheader={
            <>
              Hosted by <Link to={`/profiles/bob`}>Bob</Link>
            </>
          }
        />
        <Box sx={{ display: "flex", flexDirection: "column", gap: 2, mr: 2 }}>
          {(isHost || isGoing) && (
            <Chip label={label} color={color} sx={{ borderRadius: 2 }} />
          )}
          {isCancelled && (
            <Chip label="Cancelled" color="error" sx={{ borderRadius: 2 }} />
          )}
        </Box>
      </Box>

      <Divider sx={{ mb: 3 }} />

      <CardContent sx={{ p: 0 }}>
        <Box
          sx={{
            display: "flex",
            alignItems: "center",
            gap: 2,
            flexWrap: "nowrap",
            minWidth: 0,
            mb: 2,
            px: 2,
            pt: 2,
          }}
        >
          <Box sx={{ display: "flex", alignItems: "center" }}>
            <Box sx={{ display: "flex", alignItems: "center", flexGrow: 0 }}>
              <AccessTime sx={{ color: "text.secondary" }} />
              <Typography variant="body2" noWrap sx={{ ml: 1 }}>
                {formatDate(activity.date)}
              </Typography>
            </Box>
          </Box>
          <Typography sx={{ color: "secondary.main" }}>•</Typography>
          <Box
            sx={{
              display: "flex",
              alignItems: "center",
              gap: 1,
              flex: 1,
              minWidth: 0,
            }}
          >
            <Place sx={{ color: "text.secondary" }} />
            <Typography
              variant="body2"
              sx={{
                flex: 1,
                minWidth: 0,
                overflow: "hidden",
                textOverflow: "ellipsis",
                whiteSpace: "nowrap",
              }}
            >
              {activity.venue}
            </Typography>
          </Box>
        </Box>
        <Divider />
        <Box
          sx={{
            display: "flex",
            gap: 2,
            bgcolor: "grey.200",
            py: 3,
            pl: 3,
          }}
        >
          Attendees go here
        </Box>
      </CardContent>

      <CardActions
        sx={{ pb: 2, justifyContent: "space-between", alignItems: "center" }}
      >
        <Typography variant="body2" sx={{ flex: 1, minWidth: 0 }}>
          {activity.description}
        </Typography>
        <Button
          component={Link}
          to={`/activities/${activity.id}`}
          size="medium"
          variant="contained"
          sx={{ borderRadius: 3 }}
        >
          View
        </Button>
      </CardActions>
    </Card>
  );
}
