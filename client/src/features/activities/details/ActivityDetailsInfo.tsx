import { CalendarToday, Info, Place } from "@mui/icons-material";
import { Divider, Grid, Paper, Typography } from "@mui/material";
import { formatDate } from "../../../lib/util/util";

type Props = {
  activity: Activity;
};

export default function ActivityDetailsInfo({ activity }: Props) {
  return (
    <Paper sx={{ mb: 2 }}>
      <Grid
        container
        alignItems="center"
        wrap="nowrap"
        columnSpacing={2}
        pl={6}
        py={1}
        sx={{ minHeight: 50 }}
      >
        <Grid size="auto" sx={{ display: "flex", alignItems: "center", ml: 2 }}>
          <Info color="info" fontSize="large" />
        </Grid>
        <Grid size={11} sx={{ display: "flex", alignItems: "center" }}>
          <Typography>{activity.description}</Typography>
        </Grid>
      </Grid>
      <Divider />
      <Grid
        container
        alignItems="center"
        wrap="nowrap"
        columnSpacing={2}
        pl={6}
        py={1}
        sx={{ minHeight: 50 }}
      >
        <Grid size="auto" sx={{ display: "flex", alignItems: "center", ml: 2 }}>
          <CalendarToday color="info" fontSize="large" />
        </Grid>
        <Grid size={11} sx={{ display: "flex", alignItems: "center" }}>
          <Typography>{formatDate(activity.date)}</Typography>
        </Grid>
      </Grid>
      <Divider />
      <Grid
        container
        alignItems="center"
        wrap="nowrap"
        columnSpacing={2}
        pl={6}
        py={1}
        sx={{ minHeight: 50 }}
      >
        <Grid size="auto" sx={{ display: "flex", alignItems: "center", ml: 2 }}>
          <Place color="info" fontSize="large" />
        </Grid>
        <Grid size={11} sx={{ display: "flex", alignItems: "center" }}>
          <Typography>
            {activity.venue}, {activity.city}
          </Typography>
        </Grid>
      </Grid>
    </Paper>
  );
}
